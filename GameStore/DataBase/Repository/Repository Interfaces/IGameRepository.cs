using GameStore.Dtos;
using GameStore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameStore.DataBase.Repository
{
    public interface IGameRepository
    {
        Task<List<GameModel>> GetAllGames();
        Task<GameModel> GetGameById(int id);
        Task<GameModel> AddGame(CreateGameDto newGame);
        Task<GameModel> EditGame(EditGameDto editedGame, int id);
        Task<GameModel> DeleteGame(int id);
    }
}
