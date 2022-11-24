using GameStore.DataBase;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using GameStore.Models;
using GameStore.Services;

namespace GameStore.Controllers
{
    [Route("/[controller]")]
    public class HomeController : Controller
    {
        private readonly GameService _gameService;
        public HomeController(GameService gamesService)
        {
            _gameService = gamesService;
        }


        [HttpGet]
        public async Task<IActionResult> index()
        {
            return new OkObjectResult(await _gameService.GetAllGames()); 
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetGameById(int id)
        {
            return new OkObjectResult(await _gameService.GetGameById(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddGame([FromBody] GameModel newGame)
        {
            return new OkObjectResult(await _gameService.AddGame(newGame));
        }

        [HttpPost, Route("Edit/{id}")]
        public async Task<IActionResult> EditGame([FromBody] GameModel editedGame, int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return new OkObjectResult(await _gameService.EditGame(editedGame, id));
        }
    }
}
