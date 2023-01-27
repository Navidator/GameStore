using GameStore.CustomExceptions;
using GameStore.DataBase;
using GameStore.DataBase.Repository;
using GameStore.Dtos;
using GameStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace GameStore.Data.Repositories.GameCommentRepository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly GameStoreContext _context;
        public CommentRepository(GameStoreContext context)
        {
            _context = context;
        }

        public async Task<CommentModel> AddComment(CommentModel comment)
        {
            var gameToAddComment = await _context.Comments.AddAsync(comment);

            var entity = await _context.Comments.FirstOrDefaultAsync(x => x.CommentId == comment.CommentId);

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<int> DeleteComment(int commentId)
        {
            var commentToRemove = await _context.Comments.FirstOrDefaultAsync(x => x.CommentId == commentId);

            if (commentToRemove == null)
                throw new InvalidOperationException("Comment was not found!");

            if (commentToRemove.IsDeleted)
                throw new InvalidOperationException("The comment is already deleted");

            commentToRemove.IsDeleted = true;

            await _context.SaveChangesAsync();

            return commentId;
        }

        public IQueryable<CommentModel> GetAllComments(int gameId)
        {
            var comments = _context.Comments.Where(x => x.GameId == gameId && !x.IsDeleted && !x.IsHidden);

            return comments;
        }

        public async Task<CommentModel> GetComment(int commentId)
        {
            var comment = await _context.Comments.Include(x => x.Children).FirstOrDefaultAsync(x => x.CommentId == commentId && !x.IsDeleted);

            return comment;
        }

        public async Task<int> HideComment(int commentId)
        {
            var commentToHide = await _context.Comments.FirstOrDefaultAsync(x => x.CommentId == commentId);

            if (commentToHide == null)
                throw new InvalidOperationException("Comment was not found!");

            if (commentToHide.IsDeleted)
                throw new InvalidOperationException("The comment is deleted");

            commentToHide.IsHidden = !commentToHide.IsHidden;

            await _context.SaveChangesAsync();

            return commentId;
        }
    }
}
