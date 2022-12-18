using GameStore.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.DataBase.Repository
{
    public interface ICommentRepository
    {
        Task<CommentModel> AddComment(CommentModel comment);
        Task<int> DeleteComment(int commentId);
        Task<int> HideComment(int commentId);
        IQueryable<CommentModel> GetAllComments(int gameId);
        Task<CommentModel> GetComment(int commentId);
    }
}
