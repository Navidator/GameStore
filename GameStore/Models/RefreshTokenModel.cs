using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.Models
{
    public class RefreshTokenModel
    {
        [Key]
        public int Id { get; set; }

        public string Token { get; set; }

        public string JwtId { get; set; }

        public bool IsRevoked { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime DateExpire { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserModel")]
        public UserModel User { get; set; }
    }
}
