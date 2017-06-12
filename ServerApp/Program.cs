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
using ServerApp.CRUD;
using ViewModelDataBase.VMPublicationTypes;
using Model.UserTypes;
using Model;
using ViewModelDataBase;
using Newtonsoft.Json;
using System.Reflection;
using ViewModelDataBase.VMPublicationTypes.VMNewsTypes;
using ServerApp.Converting;

namespace ServerApp
{
    class Program
    {

        static void Main(string[] args)
        {

            var nfc = NewsForumContext.GetNewsForumContext;
            Server s = new Server();
            s.ExceptionRecived += S_ExceptionRecived;

            //nfc.Comments.Add(new DBComment()
            //{
            //    PublicationId = 2,
            //    UserId = 4,
            //    Value = "Ниче такой, с пивком потянет)"
            //});

            //nfc.Comments.Add(new DBComment()
            //{
            //    PublicationId = 2,
            //    UserId = 3,
            //    Value = "Блять, 10 из 10!"
            //});

            //nfc.Users.Add(new DBUser()
            //{
            //    AccessLevel = UserAccessLevel.User,
            //    City = "Москва",
            //    EMail = "vasy@mail.ru",
            //    Name = "Вася",
            //    Password = "qwe",
            //    Surname = "Иванов"
            //});

            //nfc.Users.Add(new DBUser()
            //{
            //    AccessLevel = UserAccessLevel.User,
            //    City = "Санкт-Питербург",
            //    Surname = "Ларин",
            //    Name = "Дмитрий",
            //    EMail = "larinChushka@mail.ru",
            //    Password = "чушка",
            //    PhoneNumber = "89994554112",
            //});

            //nfc.Users.Add(new DBUser()
            //{
            //    AccessLevel = UserAccessLevel.Admin,
            //    PhoneNumber = "89137176239",
            //    Name = "Алексей",
            //    Surname = "Нваальный",
            //    City = "Москва",
            //    EMail = "Navalnyi@mail.ru",
            //    Password = "2018"
            //});

            //nfc.Users.Add(new DBUser()
            //{
            //    Name = "Сергей",
            //    Surname = "Безруков",
            //    EMail = "SashaBelyi@yandex.ru",
            //    City = "Москва",
            //    Password = "qweqwe",
            //    AccessLevel = UserAccessLevel.Admin
            //});

            //var a = PublicationCRUD.GetSmallPublications(new RequestServer.PublicationsRequest.ReadPublciationRequest()
            //{
            //    LeftLimitTime = DateTime.Now.AddDays(-10).ToShortDateString(),
            //    RightLimitTime = DateTime.Now.ToShortDateString(),
            //    ListGenres = nfc.Genres.Where(g => g.GentreId == 16).Select(g => g.Name).ToList(),
            //    PublicationType = PublicationType.Film
            //});


            //DBPublication p = new DBPublication();
            //p.AddComment(new DBComment() { UserId = 2,PublicationId = 213 });
            //var list = (new RequestServer.PublicationsRequest.ReadPublciationRequest()
            //{
            //    LeftLimitTime = DateTime.Now.AddDays(-2).ToShortDateString(),
            //    RightLimitTime = DateTime.Now.AddDays(3).ToShortDateString(),
            //    ListGenres = GenreTypes.FilmGenres.ToList(),
            //    PublicationType = PublicationType.Film
            //});
            //var d = PublicationCRUD.GetPublication<VMGamePublication>(3);


            //foreach (var item in GenreGroup.AllGenresGroups)
            //{
            //    foreach (var genre in item.ListGenreTypes)
            //    {
            //        nfc.Genres.Add(new DBGenre() { Name = genre.Name });
            //    }
            //}

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
            //    CompanyDeveloper = "dev1",
            //    CreateDate = DateTime.Now,
            //    InterfaceLanguage = "ru1",
            //    MultiPlayer = true,
            //    Platform = "pc1",
            //    RefDescription = "desc1",
            //    RefPoster = "pos1t",
            //    Title = "titl1e",
            //    ReleaseYear = DateTime.Now.AddDays(3),
            //    ListGenres = nfc.Genres.Where(g => g.Name == "Аркада" || g.Name == "Шутер").ToList(),
            //    ListFiles = new List<DBInfoFile>()
            //    {
            //        new DBInfoFile(){FullPath = "fff", Type = "f"},
            //        new DBInfoFile(){FullPath = "sss", Type = "s"}
            //    },
            //};
            //var newPubl = nfc.GamePublications.First();
            //var files =  nfc.InfoFiles.Where(p => p.PublicatoinId == newPubl.PublicationId).ToList();
            //newPubl.Convert(publ);

            //for (int i = 0; i < files.Count; i++)
            //{
            //    nfc.InfoFiles.Remove(files[i]);
            //}
            //newPubl.ListFiles = new List<DBInfoFile>()
            //    {
            //        new DBInfoFile(){FullPath = "трололо", Type = "f"},
            //    };

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
            //    AccessLevel = Model.UserTypes.UserAccessLevel.God,
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

            //nfc.SaveChanges();
            Console.ReadKey();
        }

        private static void S_ExceptionRecived(string obj)
        {
            Console.WriteLine(obj);
        }
    }
}