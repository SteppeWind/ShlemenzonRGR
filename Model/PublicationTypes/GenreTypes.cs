using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.PublicationTypes
{
    public static class GenreTypes
    {
        public static readonly string[] FilmGenres =
        {
            "Фантастика",
            "Анимационный мультфильм",
            "Триллер",
            "Вестерн",
            "Драма",
            "Комедия",
            "Боевик",
            "Детское кино"
        };

        public static readonly string[] GameGenres = 
        {
            "Аркада",
            "Шутер",
            "Action",
            "Стратегия",
            "MMORPG",
            "Стэлс",
            "Файтинг",
            "RPG",
            "Симулятор"
        };

        public static readonly string[] MusicGenres =
        {
            "Блюз",
            "Рэпчик",
            "Джаз",
            "Шансон",
            "Рок",
            "Попса",
            "Дабстеп"
        };
    }
}