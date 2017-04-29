using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Comment
    {
        private int idComment = 0;
        public int IDComment
        {
            get { return idComment; }
            set
            {
                if (idComment == 0)
                    idComment = value;
            }
        }

        public int IDUser { get; set; }
        public string ValueComment { get; set; }

        public Comment() { }

        public Comment(int id, string value)
        {
            IDUser = id;
            ValueComment = value;
        }
    }
}