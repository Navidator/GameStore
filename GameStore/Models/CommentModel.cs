using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.Models
{
    public class CommentModel
    {
        [Key]
        public string CommentId { get; set; }

        [Required, MaxLength(600)]
        public string CommentText { get; set; }

        [Required]
        public DateTime CommentDate { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime DeletedAt { get; set; }

        //[ForeignKey("UserModel")]
        //public string UserId { get; set; }

        //[ForeignKey("GameModel")]
        //public int GameId { get; set; }



#nullable enable
        [ForeignKey("CommentModel")]
        public string? ParentId { get; set; }
#nullable disable

        public CommentModel Parent { get; set; }

        public ICollection<CommentModel> Children { get; set; } = new List<CommentModel>();

        public IList<GameAndCommentModel> GameAndComment { get; set; } = new List<GameAndCommentModel>();

        public IList<UserAndCommentModel> UserAndComment { get; set; } = new List<UserAndCommentModel>();
    }
}
