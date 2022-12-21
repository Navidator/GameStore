using GameStore.Dtos;
using GameStore.Models;
using System.Threading.Tasks;

namespace GameStore.DataBase.Repository
{
    public interface IAuthRepository
    {
        Task<UserModel> Login(LoginUserDto loginUserDto);
        Task<UserModel> Register(UserModel registerUser);
        Task<UserModel> EditUser(EditUserDto editUserDto);
        Task AddRefreshTokenAsync(RefreshTokenModel refreshToken);
        Task<RefreshTokenModel> GetRefreshTokenAsync(string token);
    }
}
