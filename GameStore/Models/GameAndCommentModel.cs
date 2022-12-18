using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Models
{
    public class GameAndCommentModel
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("GameModel")]
        public int GameId { get; set; }

        public GameModel Game { get; set; }

        [ForeignKey("CommentModel")]
        public string CommentId { get; set; }

        public CommentModel Comment { get; set; }
    }
}
