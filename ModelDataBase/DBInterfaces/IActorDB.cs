using Model.PublicationTypes;
using ModelDataBase.DBPublicationTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDataBase.DBInterfaces
{
    public interface IActorDB
    {
        int ActorId { get; set; }
    }
}