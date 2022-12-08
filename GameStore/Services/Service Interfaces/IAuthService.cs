using GameStore.Dtos;
using GameStore.Models;
using System.Threading.Tasks;

namespace GameStore.Services.Service_Interfaces
{
    public interface IAuthService
    {
        Task<UserModel> Register(RegisterUserDto registerUserDto);
    }
}
