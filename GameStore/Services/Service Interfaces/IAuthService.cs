using GameStore.Dtos;
using GameStore.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace GameStore.Services.Service_Interfaces
{
    public interface IAuthService
    {
        Task<UserModel> Register(RegisterUserDto registerUserDto);
        Task<AuthResultDto> Login(LoginUserDto loginUserDto);
        Task<UserModel> EditUser(EditUserDto editUserDto);
        Task<AuthResultDto> RefreshToken(RefreshTokenDto refreshTokenDto);
        Task SignOut();
    }
}
