using System;
using System.Collections.Generic;

namespace GameStore.Models
{
    public class GameModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public int ReleaseYear { get; set; }

        public string GameDeveloper { get; set; }

        //public List Genre
    }
}
