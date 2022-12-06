using GameStore.DataBase.Repository;

namespace GameStore.DataBase
{
    public class RepositoryProvider
    {
        public IGameRepository GameRepository { get; set; }

        public RepositoryProvider(IGameRepository gameRepository)
        {
            GameRepository = gameRepository;
        }
    }
}
