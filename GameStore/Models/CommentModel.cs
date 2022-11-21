using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.Models
{
    public class CommentModel
    {
        [Key]
        public int CommentId { get; set; }
        [Required]
        public string CommenText { get; set; }
        [Required]
        public DateTime CommentDate { get; set; }
        [ForeignKey("UserModel")]
        public int UserId { get; set; }
        [ForeignKey("GameModel")]
        public int GameId { get; set; }

    }
}
