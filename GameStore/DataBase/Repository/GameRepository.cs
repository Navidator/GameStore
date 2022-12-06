using GameStore.CustomExceptions;
using GameStore.DataBase.UnitOfWork;
using GameStore.Dtos;
using GameStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GameStore.DataBase.Repository
{
    public class GameRepository : IGameRepository
    {
        private readonly GameStoreContext _context;
        //private readonly IUnitOfWork _unitOfWork;

        public GameRepository(GameStoreContext context)
        {
            _context = context;
        }
        public Task<List<GameModel>> GetAllGames()
        {
            return _context.Games.ToListAsync();
        }

        public async Task<GameModel> GetGameById(int id)
        {
            return await _context.Games.Where(games => games.GameId == id).Include(x => x.GameAndGenre).ThenInclude(x => x.Genre).FirstOrDefaultAsync();
        }

        private IQueryable<GenreModel> GetGenres(Expression<Func<GenreModel, bool>> expression)
        {
            return _context.Genres.Where(expression);
        }

        public async Task<GameModel> AddGame(CreateGameDto newGame)
        {
            GameModel model = new GameModel
            {
                Name = newGame.Name,
                Description = newGame.Description,
                GameDeveloper = newGame.GameDeveloper,
                ImageUrl = newGame.ImageUrl,
                Price = newGame.Price,
                Publisher = newGame.Publisher,
                ReleaseDate = newGame.ReleaseDate
            };

            var genres = await GetGenres(x => newGame.GenreIds.Contains(x.GenreId)).ToListAsync();

            var gamesAndGenres = genres.Select(x => new GamesAndGenresModel
            {
                Genre = x
            });

            model.GameAndGenre = gamesAndGenres.ToList();

            await _context.Games.AddAsync(model);
            await _context.SaveChangesAsync();

            var newlyAddedGame = await _context.Games.Where(game => game.Name == newGame.Name && game.Publisher == newGame.Publisher).FirstOrDefaultAsync();
            return newlyAddedGame;
        }

        public async Task<GameModel> EditGame(EditGameDto editedGame, int id)
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

            foreach (var x in editedGame.Genres)
            {
                if (x.EditType == EditTypeValue.Add)
                {
                    gameToUpdate.GameAndGenre.Add(new GamesAndGenresModel { GenreId = x.GenreId });
                }
                else if (x.EditType == EditTypeValue.Remove)
                {
                    var y = _context.GamesAndGenres.Where(game => game.GameId == gameToUpdate.GameId && game.GenreId == x.GenreId).FirstOrDefault();

                    gameToUpdate.GameAndGenre.Remove(y);
                }
            }
            await _context.SaveChangesAsync();

            return gameToUpdate;
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
