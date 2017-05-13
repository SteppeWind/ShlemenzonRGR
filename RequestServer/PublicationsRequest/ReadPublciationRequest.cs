using Model.PublicationTypes;
using RequestServer.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestServer.PublicationsRequest
{
    public class ReadPublciationRequest
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

        public ReadPublciationRequest()
        {
            ListGenres = new List<string>();
        }
    }
}