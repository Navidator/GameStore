using GameStore.DataBase;
using GameStore.Dtos;
using GameStore.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace GameStore.Services
{
    public class OrderService
    {
        private readonly GameStoreContext _context;

        public OrderService(GameStoreContext context)
        {
            _context = context;
        }

        public async Task<List<CartModel>> GetAllOrders(int userId)
        {
            var x = await _context.Cart.Where(x => x.UserId == userId).Include(x => x.OrderedGames).ToListAsync();

            if (x != null)
            {
                return x;
            }

            return null;
        }

        public async Task<CartModel> EditCart(OrderDto dto)
        {
            if (await _context.Cart.Where(x => x.UserId == dto.UserId && x.IsPurchased == false).FirstOrDefaultAsync() == null)
            {
                var newCart = new CartModel
                {
                    UserId = dto.UserId,
                    TotalPrice = 0
                };
                await _context.AddAsync(newCart);
                await _context.SaveChangesAsync();
            };

            var y = await _context.Cart.Where(x => x.UserId == dto.UserId && x.IsPurchased == false).Include(x => x.OrderedGames).FirstOrDefaultAsync();

            if (dto.EditType == OrderEditTypeValue.Add)
            {
                var game = await _context.Games.Where(x => x.GameId == dto.GameId).FirstOrDefaultAsync();

                y.TotalPrice += game.Price;

                y.OrderedGames.Add(new OrderModel
                {
                    UserId = dto.UserId,
                    GameId = dto.GameId,
                    OrderComment = dto.Comment
                });
            }
            else if (dto.EditType == OrderEditTypeValue.Remove)
            {
                var gameToRemove = await _context.Cart.Where(x => x.UserId == dto.UserId).FirstOrDefaultAsync();
                var z = gameToRemove.OrderedGames.Where(x => x.GameId == dto.GameId).FirstOrDefault();

                var priceUpdate = await _context.Games.Where(x => x.GameId == dto.GameId).FirstOrDefaultAsync();
                y.TotalPrice -= priceUpdate.Price;

                y.OrderedGames.Remove(z);
            }
            await _context.SaveChangesAsync();

            return y;
        }

        public async Task<CartModel> Purchase(int userId, bool purchaseStatus)
        {
            var cartToPurchase = await _context.Cart.Where(x => x.UserId == userId && x.IsPurchased == false).Include(x => x.OrderedGames).FirstOrDefaultAsync();
            if (cartToPurchase != null && purchaseStatus == true)
            {
                cartToPurchase.IsPurchased = true;
            }
            await _context.SaveChangesAsync();

            return cartToPurchase;
        }
    }
}
