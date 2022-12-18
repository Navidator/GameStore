using System.ComponentModel.DataAnnotations;

namespace GameStore.Dtos
{
    public class EditCommentDto
    {
        [MaxLength(600)]
        public string CommentText { get; set; }
    }
}
