using System.ComponentModel.DataAnnotations;

namespace GameStore.Models
{
    public class OrderedGamesModel
    {
        [Key]
        public int CartId { get; set; }
        public double TotalPrice { get; set; }
    }
}
