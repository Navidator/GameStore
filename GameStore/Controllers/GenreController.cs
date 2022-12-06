using GameStore.Models;
using GameStore.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;



namespace GameStore.Controllers
{
    [Route("[controller]")]
    public class GenresController : Controller
    {
        private readonly GenresService _genresService;
        public GenresController(GenresService genresService)
        {
            _genresService = genresService;
        }



        [HttpGet]
        [Route("AllGenre")]
        public async Task<IActionResult> GetAllGenres()
        {
            return new OkObjectResult(await _genresService.GetAllGenres());
        }
        [HttpGet]
        [Route("Genre/{id}")]
        public async Task<IActionResult> GetGenreById(int id)
        {
            return new OkObjectResult(await _genresService.GetGenreById(id));
        }
        [HttpPost]
        [Route("AddGenre")]
        public async Task<IActionResult> AddGenre([FromBody] GenreModel newGenre, int? genreId)
        {
            return new OkObjectResult(await _genresService.AddGenre(newGenre, genreId));
        }



        [HttpPut]
        [Route("EditGenre/{id}")]
        public async Task<IActionResult> EditGenre([FromBody] GenreModel editedGame, int id)
        {
            return new OkObjectResult(await _genresService.EditGenre(editedGame, id));
        }




        [HttpDelete]
        [Route("DeleteGenre/{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            return new OkObjectResult(await _genresService.DeleteGenre(id));
        }
    }
}
