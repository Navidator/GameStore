using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Models
{
    public class CartModel
    {
        [Key]
        public int Id { get; set; }

        //public int GameId { get; set; }

        public int UserId { get; set; }

        public bool IsPurchased { get; set; }

        public double TotalPrice { get; set; }

        //public OrderModel game { get; set; }
        public IList<OrderModel> OrderedGames { get; set; } = new List<OrderModel>();
    }
}
