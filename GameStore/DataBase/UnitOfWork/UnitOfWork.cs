using GameStore.DataBase.Repository;
using GameStore.Services.Service_Interfaces;
using System;
using System.Threading.Tasks;

namespace GameStore.DataBase.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly GameStoreContext _context;
        public IGameRepository GameRepository { get; set; }
        public IAuthRepository AuthRepository { get; set; }
        public ICommentRepository CommentRepository { get; set; }

        public UnitOfWork(GameStoreContext context, IGameRepository gameRepository, IAuthRepository authRepository, ICommentRepository commentRepository)
        {
            _context = context;
            GameRepository = gameRepository;
            AuthRepository = authRepository;
            CommentRepository = commentRepository;
        }
        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
