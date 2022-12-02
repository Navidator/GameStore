using System;
using System.Collections.Generic;

namespace GameStore.Dtos
{
    public class CreateGameDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string GameDeveloper { get; set; }

        public string ImageUrl { get; set; }

        public string Publisher { get; set; }

        public List<int> GenreIds { get; set; }
    }
}
