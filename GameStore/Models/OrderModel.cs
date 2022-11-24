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

        [ForeignKey("GameModel")]
        public int GameId { get; set; }

        [ForeignKey("OrderedGamesModel")]
        public int ParentOrderId { get; set; }

        //public double Price { get; set; }

        [ForeignKey("CurrencyModel")]
        public int CurrencyId { get; set; }

        [ForeignKey("PaymentTypeModel")]
        public int PaymentTypeId { get; set; }

        public string OrderComment { get; set; }
    }
}
