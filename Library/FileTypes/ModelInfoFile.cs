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
    public class ModelInfoFile
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