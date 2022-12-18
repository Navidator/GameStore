using System.ComponentModel.DataAnnotations;

namespace GameStore.Dtos
{
    public class AddCommentDto
    {
        [Required, MaxLength(600)]
        public string CommentText { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int GameId { get; set; }

        public int? ParentId { get; set; }
    }
}
