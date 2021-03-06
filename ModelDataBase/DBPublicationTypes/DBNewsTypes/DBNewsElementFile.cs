﻿using Model.PublicationTypes;
using Model.PublicationTypes.NewsPublications;
using ModelDataBase.DBInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDataBase.DBPublicationTypes.DBNewsTypes
{
    [Table("DBNewsElementFiles")]
    public class DBNewsElementFile : DBNewsElement, IStoreFileDB, IInfoFile
    {
        /// <summary>
        /// Здесь может быть путь к файлу, либо ссылка на видео
        /// </summary>
        public virtual string FullPath { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }
    }
}