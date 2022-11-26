using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.Models
{
    public class GenreModel
    {
        [Key]
        public int GenreId { get; set; }
        public string GenreName { get; set; }

        [ForeignKey("GenreModel")]
        public int? ParentId { get; set; }

        public GenreModel Parent { get; set; }

        public ICollection<GenreModel> Children { get; } = new List<GenreModel>();
    }
}
