using GameStore.DataBase.UnitOfWork;
using GameStore.Dtos;
using GameStore.Models;
using GameStore.Services.Service_Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameStore.Services
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork _unitOfWork;
        public GameService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<GameModel>> GetAllGames()
        {
            return await _unitOfWork.GameRepository.GetAllGames();
        }

        public async Task<GameModel> GetGameById(int id)
        {
            return await _unitOfWork.GameRepository.GetGameById(id);
        }

        public async Task<GameModel> AddGame(CreateGameDto newGame)
        {
            return await _unitOfWork.GameRepository.AddGame(newGame);
        }

        public async Task<GameModel> EditGame(EditGameDto editedGame, int id)
        {
            return await _unitOfWork.GameRepository.EditGame(editedGame, id);
        }

        public async Task<GameModel> DeleteGame(int id)
        {
            return await _unitOfWork.GameRepository.DeleteGame(id);
        }
    }
}
