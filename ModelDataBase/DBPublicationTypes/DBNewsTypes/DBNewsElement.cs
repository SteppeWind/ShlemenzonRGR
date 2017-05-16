using Model.PublicationTypes;
using Model.PublicationTypes.NewsPublications;
using ModelDataBase.DBInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDataBase.DBPublicationTypes.DBNewsTypes
{
    public class DBNewsElement : INewsElement, IPublicationDB
    {
        private int id = -1;
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NewsElementFileId
        {
            get { return id; }
            set
            {
                if (id == -1)
                    id = value;
            }
        }


        private int publicationId = -1;
        [ForeignKey("Publication")]
        public int PublicatoinId
        {
            get => publicationId;
            set
            {
                if (publicationId == -1)
                    publicationId = value;
            }
        }

        public virtual DBPublication Publication { get; set; }

        public TypeElementOfNews TypeElement { get; set; }

        public int NumberOfList { get; set; }
    }
}