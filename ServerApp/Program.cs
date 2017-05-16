using ServerApp.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ModelDataBase.DBUserTypes;
using ModelDataBase.DBPublicationTypes;
using Model.PublicationTypes;

namespace ServerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Server s = new Server();
            s.ExceptionRecived += S_ExceptionRecived;

           // var nfc = NewsForumContext.GetNewsForumContext;
            //Database.SetInitializer(new DropCreateDatabaseAlways<NewsForumContext>());

            //List<DBGenre> genres = new List<DBGenre>()
            //{
            //    new DBGenre(){Name = GenreTypes.GameGenres[0]},
            //    new DBGenre(){Name = GenreTypes.GameGenres[1]}
            //};

            //publ.ListGenres = genres;
            //genres.ForEach(g => nfc.Genres.Add(g));
            //genres.ForEach(g => g.ListPublications.Add(publ));

            //List<DBInfoFile> files = new List<DBInfoFile>()
            //{
            //    new DBInfoFile(){FullPath = "fff", Name = "f", PublicatoinId = publ.PublicatoinId},
            //    new DBInfoFile(){FullPath="sss", Name="s", PublicatoinId = publ.PublicatoinId}
            //};
            //files.ForEach(f => nfc.InfoFiles.Add(f));

            //var publ = new ModelDataBase.DBPublicationTypes.DBGamePubliaction()
            //{
            //    CompanyDeveloper = "dev",
            //    CreateDate = DateTime.Now,
            //    InterfaceLanguage = "ru",
            //    MultiPlayer = true,
            //    Platform = "pc",
            //    RefDescription = "desc",
            //    RefPoster = "post",
            //    Title = "title",
            //    UserId = 1,
            //    ReleaseYear = DateTime.Now.AddDays(3),
            //    ListGenres = nfc.Genres.Where(g => g.Name == "Аркада" || g.Name == "Шутер").ToList(),
            //    ListFiles = new List<DBInfoFile>()
            //    {
            //        new DBInfoFile(){FullPath = "fff", Type = "f"},
            //        new DBInfoFile(){FullPath = "sss", Type = "s"}
            //    },                
            //};
            //nfc.GamePublications.Add(publ);

            //var user = nfc.Users.Where(u => u.UserId == 1).FirstOrDefault();

            //DBUser newUser = new DBUser()
            //{
            //    AccessLevel = Model.UserTypes.UserAccessLevel.User,
            //    Surname = "Пупкин",
            //    City = "Новосибирск",
            //    Name = "Василий",
            //    PhoneNumber = "1144"
            //};
            //nfc.Users.Add(newUser);


            //nfc.Users.Add(new ModelDataBase.DBUserTypes.DBUser()
            //{
            //    AccessLevel = Model.UserTypes.UserAccessLevel.Admin,
            //    City = "Нск",
            //    Name = "Vasy",
            //    Surname = "Pupkin",
            //    PhoneNumber = "898923"
            //});
            //nfc.SaveChanges();

            //nfc.FilmPublications.Add(new ModelDataBase.DBPublicationTypes.DBFilmPublication()
            //{
            //    Country = "Россия",
            //    Director = "Фонд кино",
            //    User = nfc.Users.First(),
            //    Duration = "02:24:00",
            //    Title = "Время",
            //    CreateDate = DateTime.Now
            //});

            Console.ReadKey();
        }

        private static void S_ExceptionRecived(string obj)
        {
            Console.WriteLine(obj);
        }
    }
}