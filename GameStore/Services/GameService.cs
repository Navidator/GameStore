using GameStore.CustomExceptions;
using GameStore.DataBase;
using GameStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<GameModel> GetGameById(int id)
        {
            return await _context.Games.Where(games => games.GameId == id).Include(x => x.GameAndGenre).ThenInclude(x => x.Genre).FirstOrDefaultAsync();
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

        public async Task<GameModel> AddCategoriesToGame(List<int> categoryIds, int id)
        {
            //List<int> categoryIds = new List<int>() { 2, 3, 5, 7, 8, 9, 10 }; //es gasasworebelia

            var gameToUpdate = await _context.Games.Where(game => game.GameId == id).FirstOrDefaultAsync();
            //var categoriesToAdd = new List<GamesAndGenresModel>();

            if (gameToUpdate != null)
            {
                foreach (var categoryId in categoryIds)
                {
                    var genre = await _context.Genres.FirstOrDefaultAsync(genre => genre.GenreId == categoryId);

                    //var x = new List<GamesAndGenresModel>() { new GamesAndGenresModel{ Genre = genre} };

                    gameToUpdate.GameAndGenre = new List<GamesAndGenresModel>() { new GamesAndGenresModel{ Genre = genre} }; //anu ra xdeba tamashi mogaqvs romelsac kategoriebi aqvs da shen umateb kide kategoriesb? ara. tamashs rom vaketeb es action maq rom mere kategoriebi mivamagro. tavidan tamashis gaketebastan ertad ver vqeni da ase movifiqre. tamashi rom gavakete mere vidzaxeb am funqcias da kategoriebi minda rom mivamagro mara
                }
            }
            await _context.SaveChangesAsync();

            return gameToUpdate;
        }

        public IQueryable<GenreModel> GetGenres (Expression<Func<GenreModel, bool>> expression)
        {
            return _context.Genres.Where(expression);
        }

        public async Task<GameModel> DeleteGame(int id)
        {
            GameModel gameToDelete = await _context.Games.Where(game => game.GameId == id).FirstOrDefaultAsync();
            if (gameToDelete == null)
            {
                throw new DoesNotExistException("Selected ame Could not be found");
            }
            await _context.SaveChangesAsync();

            return gameToDelete;
        }
    }
}
