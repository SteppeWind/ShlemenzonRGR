using ServerApp.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ModelDataBase.DBUserTypes;

namespace ServerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Server s = new Server();
            s.ExceptionRecived += S_ExceptionRecived;
            //var user = nfc.Users.Where(u => u.UserId == 1).FirstOrDefault();
            //DBUser newUser = new DBUser()
            //{
            //    AccessLevel = Model.UserTypes.UserAccessLevel.User,
            //    Surname = "Пупкин",
            //    City = "Новосибирск",
            //    Name = "Василий",
            //    PhoneNumber = "1144"
            //};



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
            //    Genre = "Научный",
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