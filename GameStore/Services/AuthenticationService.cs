using GameStore.CustomExceptions;
using GameStore.DataBase;
using GameStore.DataBase.UnitOfWork;
using GameStore.Dtos;
using GameStore.Models;
using GameStore.Services.Service_Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Services
{
    public class AuthenticationService : IAuthService
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly GameStoreContext _context;
        private readonly IConfiguration _configuration;

        public AuthenticationService(UserManager<UserModel> userManager, RoleManager<IdentityRole> roleManager, GameStoreContext context, IConfiguration configuration/*, IUnitOfWork unitOfWork*/)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _configuration = configuration;
            //_unitOfWork = unitOfWork;
        }

        public async Task<UserModel> Register(RegisterUserDto registerUserDto)
        {
            var user = new UserModel();

            if (registerUserDto == null)
            {
                throw new ArgumentNullException(nameof(registerUserDto));
            }
            if (await _userManager.FindByEmailAsync(registerUserDto.Email) != null)
            {
                throw new AlreadyExistException(nameof(registerUserDto));
            }
            else if (await _userManager.FindByEmailAsync(registerUserDto.Email) == null)
            {
                user.FirstName= registerUserDto.FirstName;
                user.LastName= registerUserDto.LastName;
                user.UserName= registerUserDto.UserName;
                user.Email= registerUserDto.Email;
                user.SecurityStamp = Guid.NewGuid().ToString();
            }

            StringBuilder errorMessage = new StringBuilder();

            var result = await _userManager.CreateAsync(user, registerUserDto.Password);

            if (!result.Succeeded) 
            { 
                foreach(var error in result.Errors)
                {
                    errorMessage.AppendLine(error.Description);
                }

                throw new CouldNotRegisterUserException(errorMessage.ToString());
            }

            return user;
        }

        public async Task<UserModel> Login(LoginUserDto loginUserDto)
        {
            if (loginUserDto == null)
            {
                throw new ArgumentNullException(nameof(loginUserDto));
            }

            var user = await _userManager.FindByEmailAsync(loginUserDto.email);
            var userCheck = await _userManager.CheckPasswordAsync(user, loginUserDto.password);

            if (user != null && userCheck is true)
            {
                return user;
            }

            return null;
        }
    }
}
