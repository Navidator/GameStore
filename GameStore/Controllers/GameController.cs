using GameStore.CustomExceptions;
using GameStore.Dtos;
using GameStore.Models;
using GameStore.Services;
using GameStore.TEST;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Controllers
{
    [Route("/[controller]")]
    public class GameController : Controller
    {
        private readonly GameService _gameService;
        public GameController(GameService gamesService)
        {
            _gameService = gamesService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return new OkObjectResult(await _gameService.GetAllGames());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetGameById(int id)
        {
            try
            {
                return new OkObjectResult(await _gameService.GetGameById(id));
            }
            catch (DoesNotExistException e)
            {
                return BadRequest(e.Message);
            }  
        }

        [HttpPost]
        public async Task<IActionResult> AddGame([FromBody] CreateGameDto dto)
        {
            try
            {
                //mapping should have separate class
                GameModel model = new GameModel
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    GameDeveloper = dto.GameDeveloper,
                    ImageUrl = dto.ImageUrl,
                    Price = dto.Price,
                    Publisher = dto.Publisher,
                    ReleaseDate = dto.ReleaseDate
                };

                var genres = await _gameService.GetGenres(x => dto.GenreIds.Contains(x.GenreId)).ToListAsync();

                var gamesAndGenres = genres.Select(x => new GamesAndGenresModel
                {
                    Genre = x
                });

                model.GameAndGenre = gamesAndGenres.ToList();

                //-------------------------

                return new OkObjectResult(await _gameService.AddGame(model));
            }
            catch (AlreadyExistException e)
            {
                return BadRequest(e.Message);
            }   
        }

        [HttpPut, Route("Edit/{id}")]
        public async Task<IActionResult> EditGame([FromBody] GameModel editedGame, int id)
        {
            try
            {
                return new OkObjectResult(await _gameService.EditGame(editedGame, id));
            }
            catch (DoesNotExistException e)
            {
                return BadRequest(e.Message);
            } 
        }

        [HttpPut, Route("AddCategories")]
        public async Task<IActionResult> AddCategoriesToGame([FromBody] TestClass model) //refactor TestClass
        {
            try
            {
                return new OkObjectResult(await _gameService.AddCategoriesToGame(model.CategoryIds, model.Id));
            }
            catch (DoesNotExistException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete, Route("Delete/{id}")]
        public async Task<IActionResult> Delete (int id)
        {
            try
            {
                return new OkObjectResult(await _gameService.DeleteGame(id));
            }
            catch (DoesNotExistException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
