using GameStore.DataBase;
using GameStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Services
{
    public class GameService
    {
        private readonly GameStoreContext _context;
        public GameService(GameStoreContext context)
        {
            _context = context;
        }

        public async Task<List<GameModel>> GetAllGames()
        {
            return await _context.Games.ToListAsync();
        }

        public Task<GameModel> GetGameById(int id)
        {
            return _context.Games.Where(games => games.GameId == id).FirstOrDefaultAsync();
        }

        public async Task<GameModel> AddGame(GameModel newGame)
        {
            await _context.Games.AddAsync(newGame);
            await _context.SaveChangesAsync();

            var newlyAddedGame = await _context.Games.Where(game => game.Name == newGame.Name && game.Publisher == newGame.Publisher).FirstOrDefaultAsync();
            return newlyAddedGame;
        }

        public async Task<GameModel> EditGame(GameModel editedGame, int id)
        {
            var gameToUpdate = await _context.Games.Where(game => game.GameId == id).FirstOrDefaultAsync();
            if (gameToUpdate != null)
            {
                gameToUpdate.Name = editedGame.Name;
                gameToUpdate.Description = editedGame.Description;
                gameToUpdate.GameDeveloper = editedGame.GameDeveloper;
                gameToUpdate.Publisher = editedGame.Publisher;
                gameToUpdate.Price = editedGame.Price;
                gameToUpdate.ReleaseDate = editedGame.ReleaseDate;
                gameToUpdate.ImageUrl = editedGame.ImageUrl;
            }
            await _context.SaveChangesAsync();

            return gameToUpdate;
        }
    }
}
