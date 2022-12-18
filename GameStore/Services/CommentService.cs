using GameStore.DataBase.UnitOfWork;
using GameStore.Dtos;
using GameStore.Models;
using System.Threading.Tasks;

namespace GameStore.Services
{
    public class CommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommentModel> AddComment(AddCommentDto addComment)
        {
            return await _unitOfWork.CommentRepository.
        }
    }
}
