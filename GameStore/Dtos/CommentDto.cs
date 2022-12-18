using GameStore.Models;

namespace GameStore.Dtos
{
    public class CommentDto
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public int GameId { get; set; }
        public string CommentText { get; set; }

        public CommentDto()
        {

        }

        public CommentDto(CommentModel model)
        {
            if (model == null)
                return;

            CommentId = model.CommentId;
            UserId = model.UserId;
            GameId = model.GameId;
            CommentText = model.CommentText;
        }
    }
}
