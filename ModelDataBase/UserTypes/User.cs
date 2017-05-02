using ModelDataBase.PublicationTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ModelDataBase.UserTypes
{
    class User
    {
        #region Свойства

        private int id = -1;
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId
        {
            get { return id; }
            set
            {
                if (id == -1)
                    id = value;
            }
        }
        
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
        public virtual ICollection<Rating> ListMarks { get; set; }
        
        public virtual ICollection<Comment> CommentsCollection { get; set; }
        
        public virtual ICollection<ModelPublication> ListPublications { get; set; }

        #endregion
    }
}