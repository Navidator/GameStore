using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Models
{
    public class GameModel
    {
        [Key]
        public int GameId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string GameDeveloper { get; set; }

        public string Publisher { get; set; }

        //public List Genre
    }
}
