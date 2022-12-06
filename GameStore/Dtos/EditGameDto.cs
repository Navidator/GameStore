using System.Collections.Generic;
using System;

namespace GameStore.Dtos
{
    public class EditGameDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string GameDeveloper { get; set; }

        public string ImageUrl { get; set; }

        public string Publisher { get; set; }

        public List<EditGenre> Genres { get; set; }
    }

    public class EditGenre
    {
        public EditTypeValue EditType { get; set; }
        public int GenreId { get; set; }
    }

    public enum EditTypeValue
    {
        Add, Remove
    }
}
