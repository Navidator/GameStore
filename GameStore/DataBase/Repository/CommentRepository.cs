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

            return commentId;
        }

        //public async Task<List<CommentModel>> GetAllGameAllComments()
        //{
        //    List<CommentModel> comments = await _context.Comments.ToListAsync();

        //    return comments;
        //}
        //public async Task<IList<CommentModel>> GetAllCommentsForGame(int gameId)
        //{
        //    return await _context.Comments.Where(x => x.GameId == gameId).ToListAsync();
        //}

        //public async Task<CommentViewModel> AddCommentToGame(CommentViewModel comment, Guid? parentCommentId)
        //{
        //    var commentToSave = _mapper.Map<CommentModel>(comment);
        //    ValidateOnNull<CommentModel>.ValidateDataOnNull(commentToSave);
        //    ValidateOnNullAndEmpty<string>.ValidateDataOnNullAndEmpty(commentToSave.CommentText);
        //    if (commentToSave.CommentText.Length > 600)
        //    {
        //        throw new CouldNotAddException("Text is too long!. Comment can contain only 600 characters");
        //    }
        //    commentToSave.CommentDate = DateTime.UtcNow;
        //    if (parentCommentId == null)
        //    {
        //        await _context.Comments.AddAsync(commentToSave);
        //        await _context.SaveChangesAsync();
        //        return comment;
        //    }
        //    var parentComment = await _context.Comments.Where(x => x.CommentId == parentCommentId).Include(x => x.Children).SingleOrDefaultAsync();



        //    commentToSave.ParentId = parentComment.CommentId;
        //    commentToSave.Parent = parentComment;



        //    parentComment.Children ??= new List<CommentModel>();
        //    parentComment.Children.Add(commentToSave);



        //    await _context.SaveChangesAsync();
        //    var mapToReverse = _mapper.Map<CommentViewModel>(commentToSave);
        //    return mapToReverse;
        //}
        //public async Task<CommentModel> EditComment(CommentModel comment, Guid id)
        //{
        //    if (comment.CommentText.Length > 600)
        //    {
        //        throw new CouldNotAddException("Text is too long! Comment can contain only 600 characters");
        //    }
        //    var commentToUpdate = await _context.Comments.Where(com => com.CommentId == id).FirstOrDefaultAsync();



        //    ValidateOnNull<CommentModel>.ValidateDataOnNull(commentToUpdate);
        //    commentToUpdate.CommentText = comment.CommentText;



        //    await _context.SaveChangesAsync();



        //    return commentToUpdate;
        //}
        //public async Task<CommentModel> RestoreComment(Guid commentId)
        //{
        //    var commentToRestore = await _context.Comments.Where(x => x.CommentId == commentId && x.DeletedAt != null).SingleOrDefaultAsync();



        //    ValidateOnNull<CommentModel>.ValidateDataOnNull(commentToRestore);



        //    commentToRestore.DeletedAt = null;
        //    _context.Update(commentToRestore);
        //    await _context.SaveChangesAsync();



        //    return commentToRestore;
        //}
        //public async Task<CommentModel> DeleteComment(Guid id)
        //{
        //    CommentModel comment = await _context.Comments.Where(x => x.CommentId == id).FirstOrDefaultAsync();



        //    ValidateOnNull<CommentModel>.ValidateDataOnNull(comment);



        //    _context.Comments.Remove(comment);
        //    await _context.SaveChangesAsync();



        //    return comment;
        //}
        //public async Task<CommentModel> DeleteCommentForUser(Guid id)
        //{
        //    CommentModel comment = await _context.Comments.Where(x => x.CommentId == id).FirstOrDefaultAsync();



        //    ValidateOnNull<CommentModel>.ValidateDataOnNull(comment);



        //    await _context.SaveChangesAsync();



        //    comment.DeletedAt = DateTime.UtcNow;
        //    _context.Comments.Update(comment);
        //    return comment;
        //}
    }
}
