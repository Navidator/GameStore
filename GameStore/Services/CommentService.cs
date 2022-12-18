using GameStore.DataBase.UnitOfWork;
using GameStore.Dtos;
using GameStore.Models;
using GameStore.Services.Service_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommentDto> AddComment(AddCommentDto addComment) => await AddCommentInternal(addComment, false);

        public async Task<CommentDto> AddCommentReply(AddCommentDto addComment) => await AddCommentInternal(addComment, true);

        public async Task<int> DeleteComment(int commentId)
        {
            var result = await _unitOfWork.CommentRepository.DeleteComment(commentId);

            var commitResult = await _unitOfWork.Complete();

            if (commitResult <= 0)
                throw new Exception("Operation was not commited");

            return result;
        }

        public async Task<CommentDto> EditComment(EditCommentDto editComment)
        {
            var entity = await _unitOfWork.CommentRepository.GetComment(editComment.CommentId);

            if (entity == null)
                throw new InvalidOperationException("The Comment is not found!");

            entity.CommentText = editComment.CommentText;

            var commitResult = await _unitOfWork.Complete();

            if (commitResult <= 0)
                throw new Exception("Operation was not commited");

            var dto = new CommentDto(entity);

            return dto;
        }

        public IEnumerable<CommentDto> GetAllComments(int gameId)
        {
            var comments = _unitOfWork.CommentRepository.GetAllComments(gameId).Select(x => new CommentDto(x)).ToList();

            return comments;
        }

        public async Task<int> HideComment(int commentId)
        {
            var result = await _unitOfWork.CommentRepository.HideComment(commentId);

            var commitResult = await _unitOfWork.Complete();

            if (commitResult <= 0)
                throw new Exception("Operation was not commited");

            return result;
        }

        private async Task<CommentDto> AddCommentInternal(AddCommentDto addComment, bool isReply)
        {
            var model = new CommentModel
            {
                UserId = addComment.UserId,
                GameId = addComment.GameId,
                CommentText = addComment.CommentText,
                ParentId = isReply ? addComment.ParentId : null //tu 0 iqneba gaasxavs da dasamatebelia
            };

            var result = await _unitOfWork.CommentRepository.AddComment(model);

            var commitResult = await _unitOfWork.Complete();

            if (commitResult <= 0)
                throw new Exception("Operation was not commited");

            var dto = new CommentDto(result);

            return dto;
        }
    }
}
