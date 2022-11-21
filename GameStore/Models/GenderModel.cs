using System.ComponentModel.DataAnnotations;

namespace GameStore.Models
{
    public class GenderModel
    {
        [Key]
        public int GenderId { get; set; }
        public string GenderName { get; set; }
    }
}
