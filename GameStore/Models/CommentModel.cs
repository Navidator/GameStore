using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.Models
{
    public class CommentModel
    {
        [Key]
        public int CommentId { get; set; }

        [Required, MaxLength(600)]
        public string CommentText { get; set; }

        [Required]
        public DateTime CommentDate { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsHidden { get; set; }

        public DateTime DeletedAt { get; set; }

#nullable enable
        [ForeignKey("CommentModel")]
        public int? ParentId { get; set; }
#nullable disable

        public CommentModel Parent { get; set; }

        public ICollection<CommentModel> Children { get; set; } = new List<CommentModel>();

        [ForeignKey("GameModel")]
        public int GameId { get; set; }

        public GameModel Game { get; set; }

        [ForeignKey("UserModel")]
        public int UserId { get; set; }

        public UserModel User { get; set; }
    }
}
