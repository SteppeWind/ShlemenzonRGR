using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UserTypes
{
    public interface ISmallUser
    {
        int UserId { get; set; }

        string Name { get; set; }

        string Surname { get; set; }

        string City { get; set; }

        string PhoneNumber { get; set; }

        string EMail { get; set; }

        bool IsBunned { get; set; }

        UserAccessLevel AccessLevel { get; set; }

        string Password { get; set; }
    }
}