using GameStore.CustomExceptions;
using GameStore.Models;
using GameStore.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        public async Task<IActionResult> AddGame([FromBody] GameModel newGame)
        {
            try
            {
                return new OkObjectResult(await _gameService.AddGame(newGame));
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

        [HttpPut, Route("AddCategories/{id}")]
        public async Task<IActionResult> AddCategoriesToGame([FromBody] List<int> gameAndGenre, int id)
        {
            try
            {
                return new OkObjectResult(await _gameService.AddCategoriesToGame(gameAndGenre, id));
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
