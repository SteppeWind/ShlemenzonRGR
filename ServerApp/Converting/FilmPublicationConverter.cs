using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelDataBase.DBPublicationTypes;
using ViewModelDataBase.VMPublicationTypes;
using Model.PublicationTypes;
using ServerApp.DataModel;

namespace ServerApp.Converting
{
    class FilmPublicationConverter : PublicationConverter<DBFilmPublication, VMFilmPublication>
    {
        public override VMFilmPublication ConvertPublication(DBFilmPublication publication)
        {
            var result = base.ConvertPublication(publication);
            result.ListActors = publication.ListActors.Select(a => new Actor() { Name = a.Name }).ToList();
            return result;
        }

        public override DBFilmPublication ConvertPublication(VMFilmPublication publication)
        {
            var result = base.ConvertPublication(publication);
            SaveActors(ref result, publication);
            
            return result;
        }

        
        public static void SaveActors(ref DBFilmPublication dbFilm, VMFilmPublication vmFilm)
        {
            var context = NewsForumContext.GetNewsForumContext;

            //здесь ищем список имён актеров, которые уже есть в базе, приводя к верхнему регистру
            var actors = context.Actors
                .ToList()
                .Select(a => $"{a.Name}".ToUpper())
                .Intersect(vmFilm.ListActors
                .Select(a => $"{a.Name}".ToUpper()));

            //присваиваем найденных актеров списку
            dbFilm.ListActors = context.Actors
                .Where(a => actors.Contains(a.Name.ToUpper()))
                .ToList();

            //ищем разность переданных актеров с уже найденными актерами 
            var diffActors = vmFilm.ListActors
                .Select(a => $"{a.Name}".ToUpper())
                .Except(actors);

            //добавляем актеров, которых нет в базе
            var remainingActors = vmFilm.ListActors
                .Select(a => a.Name)
                .Where(a => diffActors.Contains(a.ToUpper()))
                .ToList();

            foreach (var item in remainingActors)
            {
                dbFilm.ListActors.Add(new DBActor() { Name = item });
            }
        }
    }
}