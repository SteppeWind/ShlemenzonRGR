using Model.PublicationTypes;
using ModelDataBase.DBInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDataBase.DBPublicationTypes
{
    public class DBGenre : IGenre
    {

        private int genreId = -1;
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GentreId
        {
            get => genreId;
            set
            {
                if (genreId == -1)
                    genreId = value;
            }
        }
        
        public virtual ICollection<DBPublication> ListPublications { get; set; }

        public string Name { get ; set ; }

        public DBGenre()
        {
            ListPublications = ListPublications ?? new List<DBPublication>();
        }
    }
}