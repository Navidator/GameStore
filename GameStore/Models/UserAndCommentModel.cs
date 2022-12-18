using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.Models
{
    public class UserAndCommentModel
    {
        public int Id { get; set; }

        [ForeignKey("UserModel")]
        public string UserId { get; set; }

        public UserModel User { get; set; }

        [ForeignKey("CommentModel")]
        public string CommentId { get; set; }

        public CommentModel Comment { get; set; }
    }
}
