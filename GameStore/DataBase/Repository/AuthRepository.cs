using GameStore.CustomExceptions;
using GameStore.Dtos;
using GameStore.Models;
using Microsoft.AspNetCore.Identity;
using System.Text;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Collections.Generic;
using System.Security.Claims;

namespace GameStore.DataBase.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly GameStoreContext _context;
        private readonly IConfiguration _configuration;

        public AuthRepository(UserManager<UserModel> userManager, RoleManager<IdentityRole> roleManager, GameStoreContext context, IConfiguration configuration, SignInManager<UserModel> signInManager, TokenValidationParameters tokenValidationParameters)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _configuration = configuration;
            _signInManager = signInManager;
            _tokenValidationParameters = tokenValidationParameters;
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
                user.FirstName = registerUserDto.FirstName;
                user.LastName = registerUserDto.LastName;
                user.UserName = registerUserDto.UserName;
                user.Email = registerUserDto.Email;
                user.SecurityStamp = Guid.NewGuid().ToString();
            }

            StringBuilder errorMessage = new StringBuilder();

            var result = await _userManager.CreateAsync(user, registerUserDto.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    errorMessage.AppendLine(error.Description);
                }

                throw new CouldNotRegisterUserException(errorMessage.ToString());
            }

            return user;
        }

        public async Task<AuthResultDto> Login(LoginUserDto loginUserDto)
        {
            if (loginUserDto == null)
            {
                throw new ArgumentNullException(nameof(loginUserDto));
            }

            var user = await _userManager.FindByEmailAsync(loginUserDto.email);
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
            var user = _context.Users.Where(user => user.Email == editUserDto.Email).FirstOrDefault();
            if (editUserDto == null)
            {
                throw new ArgumentNullException(nameof(editUserDto));
            }
            else if (await _userManager.FindByEmailAsync(editUserDto.Email) != null)
            {
                user.FirstName = editUserDto.FirstName;
                user.LastName = editUserDto.LastName;
                user.UserName = editUserDto.UserName;
                user.Country = editUserDto.Country;
                user.City = editUserDto.City;
                user.ZipCode = editUserDto.ZipCode;
                user.AvatarUrl = editUserDto.AvatarUrl;

                await _context.SaveChangesAsync();

                return user;
            }
            else
                return null;
        }

        public async Task<AuthResultDto> RefreshToken(RefreshTokenDto refreshTokenDto)
        {
            if (refreshTokenDto == null)
            {
                throw new ArgumentNullException(nameof(refreshTokenDto));
            }

            var result = await VerifyAndGenerateTokenAsync(refreshTokenDto);

            return result;
        }

        public async Task SignOut() //????
        {
            await _signInManager.SignOutAsync();
        }

        private async Task<AuthResultDto> VerifyAndGenerateTokenAsync(RefreshTokenDto refreshTokenDto)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var storedToken = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == refreshTokenDto.RefreshToken);
            var user = await _userManager.FindByIdAsync(storedToken.UserId);

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

        private async Task<AuthResultDto> GenerateJWTTokenAsync(UserModel user, RefreshTokenModel rToken)
        {
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
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
            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();

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
