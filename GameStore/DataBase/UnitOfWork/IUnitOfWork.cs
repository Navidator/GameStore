using GameStore.DataBase.Repository;
using System.Threading.Tasks;

namespace GameStore.DataBase.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGameRepository GameRepository { get; }
        IAuthRepository AuthRepository { get; }
        Task<int> Complete();
    }
}
