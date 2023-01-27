using GameStore.Dtos;
using GameStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.DataBase.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly GameStoreContext _context;

        public AuthRepository(GameStoreContext context)
        {
            _context = context;
        }

        public async Task<UserModel> Login (LoginUserDto loginUserDto)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == loginUserDto.email);
        }

        public async Task<UserModel> Register(UserModel registerUser)
        {
            await _context.Users.AddAsync(registerUser);
            await _context.SaveChangesAsync();

            return registerUser;
        }

        public async Task<UserModel> EditUser(EditUserDto editUserDto)
        {
            var user = _context.Users.Where(user => user.Email == editUserDto.Email).FirstOrDefault();
            if (editUserDto == null)
            {
                throw new ArgumentNullException(nameof(editUserDto));
            }
            else if (await _context.Users.FirstOrDefaultAsync(x => x.Email == editUserDto.Email) != null)
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

        public async Task AddRefreshTokenAsync(RefreshTokenModel refreshToken) 
        {
            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();
        }
        

        public async Task<RefreshTokenModel> GetRefreshTokenAsync(string token) => await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == token);
    }
}
