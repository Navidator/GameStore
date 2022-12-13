using GameStore.Dtos;
using GameStore.Models;
using System.Threading.Tasks;

namespace GameStore.Services.Service_Interfaces
{
    public interface IAuthService
    {
        Task<UserModel> Register(RegisterUserDto registerUserDto);
        Task<AuthResultDto> Login(LoginUserDto loginUserDto);
        Task<UserModel> EditUser(EditUserDto editUserDto);
        Task<AuthResultDto> RequestNewToken(RefreshTokenDto refreshTokenDto);
        Task SignOut();
        Task<AuthResultDto> VerifyAndGenerateTokenAsync(RefreshTokenDto refreshTokenDto);
        Task<AuthResultDto> GenerateJWTTokenAsync(UserModel user, RefreshTokenModel rToken);
    }
}
