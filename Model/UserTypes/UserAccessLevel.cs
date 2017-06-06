using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UserTypes
{
    public enum UserAccessLevel
    {
        Goest = 1,
        User = 2,
        /// <summary>
        /// Модератор
        /// </summary>
        Admin = 3,
        /// <summary>
        /// Админ
        /// </summary>
        God = 4
    }
}
