using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.PublicationTypes
{
    public interface IGamePublication : INoNewsModelPublication
    {
        
        /// <summary>
        /// Разработчик
        /// </summary>
        string CompanyDeveloper { get; set; }

        /// <summary>
        /// Язык интерфейса
        /// </summary>
        string InterfaceLanguage { get; set; }

        /// <summary>
        /// Платформа игры
        /// </summary>
        string Platform { get; set; }

        /// <summary>
        /// Поддержка мультиплеера
        /// </summary>
        bool MultiPlayer { get; set; }
    }
}
