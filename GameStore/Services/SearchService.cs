using GameStore.DataBase;
using GameStore.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Services
{
    public class SearchService
    {
        private readonly GameStoreContext _context;

        public SearchService(GameStoreContext context)
        {
            _context = context;
        }

        public async Task<List<GameModel>> Search(string searchValue)
        {
            if (string.IsNullOrEmpty(searchValue))
            {
                return await _context.Games.ToListAsync();
            }

            return await SearchByName(searchValue);
        }

        public async Task<List<GameModel>> SearchByName(string searchValue)
        {
            if (string.IsNullOrEmpty(searchValue))
            {
                return await _context.Games.ToListAsync();
            }

            return await _context.Games.Where(game => game.Name.Contains(searchValue)).Include(x => x.GameAndGenre).ThenInclude(x => x.Genre).ToListAsync();
        }

        public async Task<List<GameModel>> FilterByGenre(int genreId)
        {
            var retreivedGamesByCategodyId = await _context.GamesAndGenres.Where(genre => genre.GenreId.Equals(genreId)).ToListAsync();

            return null;
        }
    }
}
