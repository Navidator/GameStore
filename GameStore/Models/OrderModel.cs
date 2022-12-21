using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.Models
{
    public class OrderModel
    {
        [Key]
        public int OrderId { get; set; }

        [ForeignKey("UserModel")]
        public int UserId { get; set; }

#nullable enable
        [ForeignKey("GameModel")]
        public int GameId { get; set; }
#nullable disable

        public string OrderComment { get; set; }

        [ForeignKey("CartModel")]
        public int? ParentId { get; set; }

        public CartModel Parent { get; set; }
    }
}
