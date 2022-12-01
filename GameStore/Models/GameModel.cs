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
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public string GameDeveloper { get; set; }

        public string ImageUrl { get; set; }

        public string Publisher { get; set; }

        public IList<GamesAndGenresModel> GameAndGenre { get; set; } = new List<GamesAndGenresModel>();
    }
}
