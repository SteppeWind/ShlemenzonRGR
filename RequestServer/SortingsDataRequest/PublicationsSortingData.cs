using Model.PublicationTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelDataBase.VMPublicationTypes;

namespace RequestServer.PublicationsRequest.SortingsDataRequest
{
    /// <summary>
    /// Класс для передачи запросов серверу (используются простые типы данных для упрощения транспортировки)
    /// </summary>
    public class PublicationsSortingData
    {
        /// <summary>
        /// Левая границы даты
        /// </summary>
        public string LeftLimitTime { get; set; }


        /// <summary>
        /// Правая граница даты
        /// </summary>
        public string RightLimitTime { get; set; }

        /// <summary>
        /// Список жанров 
        /// </summary>
        public List<string> ListGenres { get; set; }

        public PublicationType PublicationType { get; set; }

        public PublicationsSortingData()
        {
            ListGenres = new List<string>();
        }
    }
}