using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.Models
{
    public class GenreModel
    {
        [Key]
        public int GenreId { get; set; }
        public string  GenreName { get; set; }
        public string ParentId { get; set; }
    }
}
