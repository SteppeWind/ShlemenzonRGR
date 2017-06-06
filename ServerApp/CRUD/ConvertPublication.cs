using Model.PublicationTypes;
using Model.PublicationTypes.NewsPublications;
using ModelDataBase.DBPublicationTypes;
using ModelDataBase.DBPublicationTypes.DBNewsTypes;
using ModelDataBase.DBUserTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelDataBase;
using ViewModelDataBase.VMPublicationTypes;
using ViewModelDataBase.VMPublicationTypes.VMNewsTypes;
using ViewModelDataBase.VMTypes;

namespace ServerApp.CRUD
{
    public class ConvertPublication
    {

        public static VMPublication GetVMPublication(DBPublication publication)
        {
            List<VMFile> files = new List<VMFile>();
            List<NewsElement> newsFiles = new List<NewsElement>();
            VMPublication result = null;
            if (publication.TypePublication == PublicationType.News)
            {
                var newsPublication = new VMNewsPublication()
                {
                    PublicationComponent = publication,
                    PublicationId = publication.PublicationId
                };
                var elements = (publication as DBNewsPublication).ListElements;
                foreach (var item in elements)
                {
                    var currElemenet = new NewsElement()
                    {
                        PublicationId = publication.PublicationId,
                        NumberOfList = item.NumberOfList
                    };
                    DBNewsElementFile currFile = (item as DBNewsElementFile);
                    switch (item.TypeElement)
                    {

                        case Model.PublicationTypes.NewsPublications.TypeElementOfNews.LinkVideo:
                            currElemenet = new VMElementLinkVideo()
                            {
                                PublicationId = publication.PublicationId,
                                NumberOfList = item.NumberOfList,
                                LinkYoutubeVideo = currFile.FullPath
                            };
                            break;

                        case Model.PublicationTypes.NewsPublications.TypeElementOfNews.Image:
                        case Model.PublicationTypes.NewsPublications.TypeElementOfNews.Text:
                            currElemenet = new VMNewsElementFile()
                            {
                                Bytes = FilesManipulation.ConvertFileToBytes(currFile.FullPath),
                                Type = currFile.Type,
                                NumberOfList = item.NumberOfList,
                                PublicationId = publication.PublicationId
                            };
                            break;
                    }
                }
                result = newsPublication;
            }

            if (result == null)
            {
                foreach (var item in publication.ListFiles)
                {
                    files.Add(new VMFile()
                    {
                        Bytes = FilesManipulation.ConvertFileToBytes(item.FullPath),
                        Type = item.Type
                    });
                }
               result = new VMPublication()
                {
                    ListFiles = files,
                    Description = new VMFile()
                    {
                        Bytes = FilesManipulation.ConvertFileToBytes(publication.RefDescription),
                        Type = ".rtf"
                    },
                };
            }

            if (result != null && publication != null)
                IntermediateСonversion(ref result, ref publication);

            return result;
        }

        private static void IntermediateСonversion(ref VMPublication vmpublic, ref DBPublication dbpublic)
        {
            vmpublic.ListGenres = dbpublic.ListGenres.ToList<Genre>();
            vmpublic.ListMarks = dbpublic.ListMarks.ToList<Rating>();
            vmpublic.ListComments = dbpublic.ListComments.ToList<Comment>();
            vmpublic.PublicationId = dbpublic.PublicationId;
            vmpublic.PublicationComponent = dbpublic;
            vmpublic.PosterImage = new VMFile()
            {
                Bytes = FilesManipulation.ConvertFileToBytes(dbpublic.RefPoster),
                Type = $"{dbpublic.RefPoster.Substring(dbpublic.RefPoster.LastIndexOf('.'))}"
            };
        }

        //public static VMUser GetVMUser(DBUser user)
        //{

        //}
    }
}