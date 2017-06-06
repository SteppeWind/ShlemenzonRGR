using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.PublicationTypes
{
    public class SmallPublication : ConvertProperty, ISmallPublication
    {

        protected SmallPublication publicationComponent;
        public virtual SmallPublication PublicationComponent
        {
            get
            {
                if (publicationComponent == null)
                {
                    publicationComponent = new SmallPublication();
                    Convert(publicationComponent, this);
                }
                return publicationComponent;
            }
            set
            {
                publicationComponent = value;
                Convert(publicationComponent);
            }
        }
        
        [Property("UserId")]
        public virtual int UserId { get; set; }
        
        public virtual int PublicationId { get; set; }
        
        /// <summary>
        /// Заголовок публикации
        /// </summary>
        [Property("Title")]
        public virtual string Title { get; set; }

        [Property("TypePublication")]
        public virtual PublicationType TypePublication { get; set; } = PublicationType.Game;

        /// <summary>
        /// Дата создания публикации
        /// </summary>
        [Property("CreateDate")]
        public virtual DateTime CreateDate { get; set; }

        /// <summary>
        /// Указывает на то, удалена ли публикация
        /// </summary>
        [Property("IsDeleted")]
        public virtual bool IsDeleted { get; set; } = false;

        /// <summary>
        /// Указывает на то, опубликована ли публикация (по умолчанию false, т.е. не опубликована)
        /// </summary>
        [Property("IsPublished")]
        public virtual bool IsPublished { get; set; } = false;

        public SmallPublication()
        {
            if (CreateDate == DateTime.MinValue)
                CreateDate = DateTime.Now;
        }
    }
}