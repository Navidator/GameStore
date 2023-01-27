using GameStore.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameStore.Services.Service_Interfaces
{
    public interface ICommentService
    {
        Task<CommentDto> AddComment(AddCommentDto addComment);
        Task<CommentDto> AddCommentReply(AddCommentDto addComment);
        Task<CommentDto> EditComment(EditCommentDto editComment);
        Task<int> DeleteComment(int commentId);
        Task<int> HideComment(int commentId);
        IEnumerable<CommentDto> GetAllComments(int gameId);
    }
}
