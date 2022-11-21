using System.ComponentModel.DataAnnotations;

namespace GameStore.Models
{
    public class CurrencyModel
    {
        [Key]
        public int CurrencyId { get; set; }
        public string CurrencyName { get; set; }
    }
}
