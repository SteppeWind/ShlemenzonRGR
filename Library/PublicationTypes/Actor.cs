using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDataBase.PublicationTypes
{
    public class Actor
    {
        private int actorId = -1;
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ActorId
        {
            get { return actorId; }
            set
            {
                if (actorId == -1)
                    actorId = value;
            }
        }

        public virtual string Name { get; set; }

        public virtual ICollection<FilmModelPublication> ListFilms { get; set; }
    }
}