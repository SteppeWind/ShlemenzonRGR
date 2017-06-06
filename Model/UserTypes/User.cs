using Model.PublicationTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UserTypes
{
    public class User : ConvertProperty, ISmallUser
    {
        #region Свойства

        public virtual int UserId { get; set; }

        [Property("Name")]
        public virtual string Name { get; set; }

        [Property("Surname")]
        public virtual string Surname { get; set; }

        [Property("City")]
        public virtual string City { get; set; }

        [Property("PhoneNumber")]
        public virtual string PhoneNumber { get; set; }

        /// <summary>
        /// Указывает на то, забанен ли пользователь 
        /// </summary>
        [Property("IsBunned")]
        public virtual bool IsBunned { get; set; } = false;

        /// <summary>
        /// Уровень доступа
        /// </summary>
        [Property("AccessLevel")]
        public virtual UserAccessLevel AccessLevel { get; set; } = UserAccessLevel.Goest;

        [Property("Password")]
        public string Password { get; set; }

        [Property("EMail")]
        public string EMail { get; set; }


        //protected User user { get; set; }
        //public virtual User UserComponent
        //{
        //    get
        //    {
        //        if (user == null)
        //        {
        //            user = new User();
        //            Convert(user, this);
        //        }
        //        return user;
        //    }
        //    set
        //    {
        //        if (value != null)
        //        {
        //            user = value;
        //            Convert(value);
        //        }
        //    }
        //}

        public virtual bool IsCorrectUserForAutorize => EMail != null && Password != null && Name != null && Surname != null;

        #endregion
    }
}