using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.PublicationTypes
{
    public class GenreGroup
    {
        public string GroupName { get; set; }

        public IEnumerable<Genre> ListGenreTypes { get; set; }

        public static GenreGroup FilmGenreGroup => new GenreGroup()
        {
            GroupName = "Фильмы",
            ListGenreTypes = GenreTypes.FilmGenres.Select(g => new Genre() { Name = g })
        };

        public static GenreGroup MusicGenreGroup => new GenreGroup()
        {
            GroupName = "Музыка",
            ListGenreTypes = GenreTypes.MusicGenres.Select(g => new Genre() { Name = g })
        };

        public static GenreGroup GameGenreGroup => new GenreGroup()
        {
            GroupName = "Игры",
            ListGenreTypes = GenreTypes.GameGenres.Select(g => new Genre() { Name = g })
        };

        public static List<GenreGroup> AllGenresGroups => new List<GenreGroup>() { GameGenreGroup, FilmGenreGroup, MusicGenreGroup };
    }
}