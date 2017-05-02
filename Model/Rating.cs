using NewsForum.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Rating
    {
        [ForeignKey("User")]
        public int IDUser { get; set; }

        private User user;
        public User User
        {
            get { return user; }
            set
            {
                user = value;
            }
        }

        public int Mark { get; set; }
    }
}