using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.DataModel;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var lc = new LibraryContext())
            {
                Book book = new Book()
                {
                    Title = "Война и мир",
                    Autor = "Лев Толстой"
                };

                lc.Books.Add(book);
                lc.SaveChanges();
            }
        }
    }
}