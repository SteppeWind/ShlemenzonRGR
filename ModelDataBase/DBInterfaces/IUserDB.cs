using ModelDataBase.DBUserTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDataBase.DBInterfaces
{
    public interface IUserDB
    {
        int UserId { get; set; }
        DBUser User { get; set; }
    }
}