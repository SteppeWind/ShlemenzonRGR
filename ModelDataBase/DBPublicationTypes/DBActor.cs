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
    public class DBActor : Actor, IActorDB
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
        
        public virtual ICollection<DBFilmPublication> ListFilms { get; set; }
    }
}