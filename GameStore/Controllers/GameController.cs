using GameStore.CustomExceptions;
using GameStore.Dtos;
using GameStore.Services.Service_Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GameStore.Controllers
{
    [Route("/[controller]")]
    public class GameController : Controller
    {
        private readonly IGameService _gameService;
        public GameController(IGameService gamesService)
        {
            _gameService = gamesService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return new OkObjectResult(await _gameService.GetAllGames());
        }

        [Authorize]
        [HttpGet, Route("GetGame/{id}")]
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

        [Authorize]
        [HttpPost, Route("AddGame")]
        public async Task<IActionResult> AddGame([FromBody] CreateGameDto dto)
        {
            try
            {
                return new OkObjectResult(await _gameService.AddGame(dto));
            }
            catch (AlreadyExistException e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpPut, Route("Edit/{id}")]
        public async Task<IActionResult> EditGame([FromBody] EditGameDto editedGame, int id)
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

        [Authorize]
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
