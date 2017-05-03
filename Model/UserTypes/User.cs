using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UserTypes
{
    public class User
    {
        #region Свойства
        
        public virtual string Name { get; set; }

        public virtual string Surname { get; set; }

        public virtual string City { get; set; }

        public virtual string PhoneNumber { get; set; }
        /// <summary>
        /// Указывает на то, забанен ли пользователь 
        /// </summary>
        public bool IsBunned { get; set; } = false;

        /// <summary>
        /// Уровень доступа
        /// </summary>
        public virtual UserAccessLevel AccessLevel { get; set; }

        /// <summary>
        /// Общий список оценок публикации
        /// </summary>
        //public virtual ICollection<Rating> ListMarks { get; set; }

        //public virtual ICollection<Comment> CommentsCollection { get; set; }

        //public virtual ICollection<T> ListPublications { get; set; }

        #endregion
    }
}