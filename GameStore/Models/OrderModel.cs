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

        public List<int> GameId { get; set; }  //To be refactored if needed

        public double TotalPrice { get; set; }

        [ForeignKey("CurrencyModel")]
        public int CurrencyId { get; set; }

        [ForeignKey("PaymentTypeModel")]
        public int PaymentTypeId { get; set; }

        public string OrderComment { get; set; }
    }
}
