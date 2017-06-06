using Model.PublicationTypes.NewsPublications;
using ModelDataBase.DBPublicationTypes.DBNewsTypes;
using ServerApp.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelDataBase.VMPublicationTypes.VMNewsTypes;

namespace ServerApp.Converting
{
    class NewsPublicationConvert : PublicationConverter<DBNewsPublication, VMNewsPublication>
    {
        public override VMNewsPublication ConvertPublication(DBNewsPublication publication)
        {
            var result = base.ConvertPublication(publication);
            foreach (var item in publication.ListElements)
            {
                var currElemenet = new NewsElement()
                {
                    PublicationId = publication.PublicationId,
                    NumberOfList = item.NumberOfList
                };
                DBNewsElementFile currFile = (item as DBNewsElementFile);
                switch (item.TypeElement)
                {

                    case TypeElementOfNews.LinkVideo:
                        currElemenet = new VMElementLinkVideo()
                        {
                            PublicationId = publication.PublicationId,
                            NumberOfList = item.NumberOfList,
                            LinkYoutubeVideo = currFile.FullPath
                        };
                        break;

                    case TypeElementOfNews.Image:
                    case TypeElementOfNews.Text:
                        currElemenet = new VMNewsElementFile()
                        {
                            Bytes = FilesManipulation.ConvertFileToBytes(currFile.FullPath),
                            Type = currFile.Type,
                            TypeElement = item.TypeElement,
                            NumberOfList = item.NumberOfList,
                            PublicationId = publication.PublicationId
                        };
                        break;
                }
                result.ListElements.Add(currElemenet);
            }

            return result;
        }

        public DBNewsPublication ConvertAndCreatePublication(string pathFiles, VMNewsPublication publication)
        {
            var result = base.ConvertPublication(publication);

            CreateAndSaveListElements(ref result, publication, pathFiles);

            return result;
        }

        public static void CreateAndSaveListElements(ref DBNewsPublication dbPublic, VMNewsPublication vmPublic, string pathFiles)
        {
            var pathNewsPublicationFiles = pathFiles;
            foreach (var item in vmPublic.ListElements)
            {
                DBNewsElement currDbNewsEl = new DBNewsElement()
                {
                    NumberOfList = item.NumberOfList,
                    TypeElement = item.TypeElement
                };
                string pathFile = $"{pathNewsPublicationFiles}{item.NumberOfList}";
                switch (item.TypeElement)
                {
                    case TypeElementOfNews.Image:
                    case TypeElementOfNews.Text:
                        VMNewsElementFile currNewsElFile = item as VMNewsElementFile;
                        pathFile += currNewsElFile.Type;
                        FilesManipulation.CreateFile(currNewsElFile.Bytes, pathFile);
                        currDbNewsEl = new DBNewsElementFile()
                        {
                            TypeElement = item.TypeElement,
                            Type = currNewsElFile.Type,
                            NumberOfList = item.NumberOfList,
                            FullPath = pathFile
                        };
                        break;

                    case TypeElementOfNews.LinkVideo:
                        VMElementLinkVideo linkVideoEl = item as VMElementLinkVideo;
                        currDbNewsEl = new DBNewsElementFile()
                        {
                            NumberOfList = item.NumberOfList,
                            FullPath = linkVideoEl.LinkYoutubeVideo,
                            TypeElement = TypeElementOfNews.LinkVideo
                        };
                        break;

                    default:
                        break;
                }
                dbPublic.ListElements.Add(currDbNewsEl);
            }
        }
    }
}