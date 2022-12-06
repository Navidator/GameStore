using GameStore.CustomExceptions;
using GameStore.DataBase;
using GameStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Services
{
    public class GenresService
    {
        private readonly GameStoreContext _context;
        public GenresService(GameStoreContext context)
        {
            _context = context;
        }

        public async Task<List<GenreModel>> GetAllGenres()
        {
            List<GenreModel> genres = await _context.Genres.ToListAsync();
            if (genres.Count == 0)
            {
                throw new DoesNotExistException("Genres Not Found");
            }
            return genres;
        }

        public async Task<GenreModel> GetGenreById(int id)
        {
            GenreModel genre = await _context.Genres.Where(genres => genres.GenreId == id).FirstOrDefaultAsync();
            if (genre == null)
            {
                throw new DoesNotExistException("Genre not found with that ID");
            }
            return genre;
        }

        public async Task<GenreModel> AddGenre(GenreModel genre, int? genreId)
        {
            if (genre == null)
            {
                throw new ArgumentNullException(nameof(genre));
            }
            if (genreId == null)
            {
                if (await _context.Genres.Where(x => x.GenreName == genre.GenreName).AnyAsync())
                {
                    throw new AlreadyExistException();
                }
                await _context.Genres.AddAsync(genre);
                await _context.SaveChangesAsync();
                return genre;
            }
            var parentGenre = await _context.Genres.Where(x => x.GenreId == genreId).Include(x => x.Children).SingleOrDefaultAsync();
            genre.ParentId = parentGenre.GenreId;
            genre.Parent = parentGenre;
            parentGenre.Children ??= new List<GenreModel>();
            parentGenre.Children.Add(genre);
            await _context.SaveChangesAsync();
            return genre;
        }

        public async Task<GenreModel> EditGenre(GenreModel editedGenre, int id)
        {
            var genreToUpdate = await _context.Genres.Where(genres => genres.GenreId == id).FirstOrDefaultAsync();
            if (genreToUpdate == null)
            {
                throw new DoesNotExistException("Can not find a genre with that ID to edit");
            }
            genreToUpdate.GenreName = editedGenre.GenreName;
            await _context.SaveChangesAsync();

            return genreToUpdate;
        }

        public async Task<GenreModel> DeleteGenre(int id)
        {
            GenreModel findGenre = await _context.Genres.Where(x => x.GenreId == id).FirstOrDefaultAsync();
            if (findGenre == null)
            {
                throw new DoesNotExistException("Can not find a genre to delete");
            }
            _context.Genres.Remove(findGenre);

            await _context.SaveChangesAsync();
            return findGenre;
        }
    }
}
