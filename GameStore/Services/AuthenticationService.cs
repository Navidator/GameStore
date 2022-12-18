using GameStore.CustomExceptions;
using GameStore.DataBase.UnitOfWork;
using GameStore.Dtos;
using GameStore.Models;
using GameStore.Services.Service_Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Services
{
    public class AuthenticationService : IAuthService
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public AuthenticationService(UserManager<UserModel> userManager, IConfiguration configuration, TokenValidationParameters tokenValidationParameters, IUnitOfWork unitOfWork, SignInManager<UserModel> signInManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
            _tokenValidationParameters = tokenValidationParameters;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserModel> Register(RegisterUserDto registerUserDto)
        {
            var user = new UserModel();

            if (registerUserDto == null)
            {
                throw new ArgumentNullException(nameof(registerUserDto));
            }
            if (await _userManager.FindByEmailAsync(registerUserDto.Email) != null && await _userManager.FindByNameAsync(registerUserDto.UserName) != null)
            {
                throw new AlreadyExistException(nameof(registerUserDto));
            }
            else if (await _userManager.FindByEmailAsync(registerUserDto.Email) == null)
            {
                var passwordHash = new PasswordHasher<UserModel>();
                user.FirstName = registerUserDto.FirstName;
                user.LastName = registerUserDto.LastName;
                user.UserName = registerUserDto.UserName;
                user.Email = registerUserDto.Email;
                user.SecurityStamp = Guid.NewGuid().ToString();
                user.PasswordHash = passwordHash.HashPassword(user, registerUserDto.Password);
            }

            return await _unitOfWork.AuthRepository.Register(user);
        }

        public async Task<AuthResultDto> Login(LoginUserDto loginUserDto)
        {
            if (loginUserDto == null)
            {
                throw new ArgumentNullException(nameof(loginUserDto));
            }

            var user = await _unitOfWork.AuthRepository.Login(loginUserDto);
            var userCheck = await _userManager.CheckPasswordAsync(user, loginUserDto.password);

            if (user != null && userCheck is true)
            {
                var tokenValue = await GenerateJWTTokenAsync(user, null);

                return tokenValue;
            }

            return null;
        }

        public async Task<UserModel> EditUser(EditUserDto editUserDto)
        {
            return await _unitOfWork.AuthRepository.EditUser(editUserDto);
        }

        public async Task<AuthResultDto> RequestNewToken(RefreshTokenDto refreshTokenDto)
        {
            var refreshToken = await VerifyAndGenerateTokenAsync(refreshTokenDto);

            return refreshToken;
        }

        public async Task SignOut() //????
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<AuthResultDto> VerifyAndGenerateTokenAsync(RefreshTokenDto refreshTokenDto)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var storedToken = await _unitOfWork.AuthRepository.GetRefreshTokenAsync(refreshTokenDto.RefreshToken);
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == storedToken.UserId);

            try
            {
                var tokenCheckResult = jwtTokenHandler.ValidateToken(refreshTokenDto.Token, _tokenValidationParameters, out var validToken);

                return await GenerateJWTTokenAsync(user, storedToken);
            }
            catch (SecurityTokenExpiredException)
            {
                if (storedToken.DateExpire >= DateTime.UtcNow)
                {
                    return await GenerateJWTTokenAsync(user, storedToken);
                }
                else
                {
                    return await GenerateJWTTokenAsync(user, null);
                }
            }
        }

        public async Task<AuthResultDto> GenerateJWTTokenAsync(UserModel user, RefreshTokenModel rToken)
        {
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var authSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.UtcNow.AddMinutes(5),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            if (rToken != null)
            {
                var rTokenResponse = new AuthResultDto()
                {
                    Token = jwtToken,
                    RefreshToken = rToken.Token,
                    ExpiresAt = token.ValidTo
                };
                return rTokenResponse;
            }

            var refreshToken = new RefreshTokenModel()
            {
                JwtId = token.Id,
                IsRevoked = false,
                UserId = user.Id,
                DateAdded = DateTime.UtcNow,
                DateExpire = DateTime.UtcNow.AddMonths(6),
                Token = Guid.NewGuid().ToString() + "-" + Guid.NewGuid().ToString()
            };

            await _unitOfWork.AuthRepository.AddRefreshTokenAsync(refreshToken);
            await _unitOfWork.Complete();

            var response = new AuthResultDto()
            {
                Token = jwtToken,
                RefreshToken = refreshToken.Token,
                ExpiresAt = token.ValidTo
            };
            return response;
        }
    }
}
