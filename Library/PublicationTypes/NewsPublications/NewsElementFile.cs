using ModelDataBase.PublicationTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.PublicationTypes.NewsPublications
{
    //СУКА ОБЯЗАТЕЛЬНО ИСПРАВЬ ЭТО ДЕРЬМО (у тебя уже есть класс, реализующий весь необходимый функционал)
    class NewsElementFile : NewsElement
    {
        internal virtual string FullPath { get; set; }
        public virtual string Name { get; set; }

        private int id = -1;
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get { return id; }
            set
            {
                if (id == -1)
                    id = value;
            }
        }

        [ForeignKey("Publication")]
        public int PublicationId { get; set; }

        public virtual ModelPublication Publication { get; set; }
    }
}