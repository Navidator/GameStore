using GameStore.Models;
using System.Collections.Generic;

namespace GameStore.Dtos
{
    public class OrderDto
    {
        public int GameId { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
        public OrderEditTypeValue EditType { get; set; }
    }

    public enum OrderEditTypeValue
    {
        Add, Remove
    }
}
