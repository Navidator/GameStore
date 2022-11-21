using GameStore.Enums;
using System.Collections.Generic;

namespace GameStore.Models
{
    public class OrderModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public List<int> GameId { get; set; }

        public double TotalPrice { get; set; }

        public string Currency { get; set; }

        public PaymentType PaymentType { get; set; }

        public string OrderComment { get; set; }
    }
}
