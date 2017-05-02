using ModelDataBase.PublicationTypes;
using ModelDataBase.PublicationTypes.NewsPublications;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDataBase.FileTypes
{
    abstract class ModelInfoFile
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

        public int NumberInList { get; set; }

        public TypeElementOfNews TypeElement { get; set; }

        [ForeignKey("Publication")]
        public int PublicationId { get; set; }

        public virtual ModelPublication Publication { get; set; }
    }
}