using ModelDataBase.UserTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDataBase.PublicationTypes
{
    class ModelPublication
    {
        #region Свойства

        private int id = -1;
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PublicationId
        {
            get { return id; }
            set
            {
                if (id == -1)
                    id = value;
            }
        }

        private int idUser = -1;
        [ForeignKey("User")]
        public int UserId
        {
            get { return idUser; }
            set
            {
                if (idUser == -1)
                    idUser = value;
            }
        }

        protected User user;
        public virtual User User
        {
            get { return user; }
            set
            {
                user = value;
            }
        }

        private string title;
        /// <summary>
        /// Заголовок публикации
        /// </summary>
        public virtual string Title
        {
            get { return title; }
            set
            {
                title = value;
            }
        }

        private PublicationType type;
        public virtual PublicationType TypePublication
        {
            get { return type; }
            set
            {
                type = value;
            }
        }

        private string refDecription;
        /// <summary>
        /// Ссылка на файл с описанием (полный путь к файлу на сервере)
        /// </summary>
        internal string RefDecription
        {
            get { return refDecription; }
            set { refDecription = refDecription ?? value; }
        }

        private string refCover;
        /// <summary>
        /// Ссылка на файл с обложкой публикации (полный путь к файлу на сервере)
        /// </summary>
        internal string RefCover
        {
            get { return refCover; }
            set { refCover = refCover ?? value; }
        }

        private DateTime createDate;
        /// <summary>
        /// Дата создания публикации
        /// </summary>
        public virtual DateTime CreateDate
        {
            get { return createDate; }
            set
            {
                createDate = value;
            }
        }
        
        /// <summary>
        /// Общий список оценок публикации
        /// </summary>
        public ICollection<Rating> ListMarks { get; set; }

        public ICollection<Comment> CommentsCollection { get; set; }

        #endregion
    }
}