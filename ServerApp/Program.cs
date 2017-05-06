using ServerApp.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ServerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            NewsForumContext nfc = new NewsForumContext();

            //nfc.Users.Add(new ModelDataBase.DBUserTypes.DBUser()
            //{
            //    AccessLevel = Model.UserTypes.UserAccessLevel.Admin,
            //    City = "Нск",
            //    Name = "Vasy",
            //    Surname = "Pupkin",
            //    PhoneNumber = "898923"
            //});


            //nfc.FilmPublications.Add(new ModelDataBase.DBPublicationTypes.DBFilmPublication()
            //{
            //    Country = "Россия",
            //    Director = "Фонд кино",
            //    Genre = "Научный",
            //    User = nfc.Users.First(),
            //    Duration = "02:24:00",
            //    Title = "Время",
            //    CreateDate = DateTime.Now
            //});


            nfc.SaveChanges();
        }
    }
}