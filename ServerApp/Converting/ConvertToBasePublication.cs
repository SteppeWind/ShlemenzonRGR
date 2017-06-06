using ModelDataBase.DBPublicationTypes;
using ServerApp.CRUD;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelDataBase.VMPublicationTypes;
using ViewModelDataBase.VMTypes;

namespace ServerApp.Converting
{
    class ConvertToBasePublication
    {
        public static VMSmallPublication GetBasePublication(DBPublication publication)
        {
            VMSmallPublication result = new VMSmallPublication()
            {
                PublicationId = publication.PublicationId,
                PosterImage = new VMFile()
            };
            result.Convert(publication);
            if (publication.RefPoster != null)
            {
                result.PosterImage = new VMFile()
                {
                    Bytes = FilesManipulation.ConvertFileToBytes(publication.RefPoster),
                    Type = Path.GetExtension(publication.RefPoster.Substring(1))
                };
            }

            return result;
        }
    }
}