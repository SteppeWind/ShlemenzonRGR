﻿using Model.UserTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.PublicationTypes
{
    public class Publication
    {
        #region Свойства
        
        /// <summary>
        /// Заголовок публикации
        /// </summary>
        public virtual string Title { get; set; }

        public virtual PublicationType TypePublication { get; set; } = PublicationType.Game;
        
        /// <summary>
        /// Дата создания публикации
        /// </summary>
        public virtual DateTime CreateDate { get; set; }

        /// <summary>
        /// Указывает на то, удалена ли публикация
        /// </summary>
        public virtual bool IsDeleted { get; set; } = false;
        

        #endregion
    }
}