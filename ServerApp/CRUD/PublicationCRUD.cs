using ServerApp.DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelDataBase.VMPublicationTypes;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Text;
using Windows.Storage.Streams;
using Windows.Storage;
using Windows.Foundation;
using Model.PublicationTypes;
using ViewModelDataBase.VMPublicationTypes.VMNewsTypes;
using ModelDataBase.DBPublicationTypes;
using ModelDataBase.DBUserTypes;
using Model.UserTypes;
using Windows.Graphics.Imaging;
using System.Runtime.Serialization.Formatters.Binary;
using ViewModelDataBase.VMInterfaces;
using ModelDataBase.DBPublicationTypes.DBNewsTypes;
using Model.PublicationTypes.NewsPublications;

namespace ServerApp.CRUD
{
    public static class PublicationCRUD
    {
        private static NewsForumContext CurrentNewsForumContext = NewsForumContext.GetNewsForumContext;

        public static bool CreatePublication(VMPublication publication)
        {
            bool result = false;
            try
            {
                var user = CurrentNewsForumContext.Users.FirstOrDefault(u => publication.User.UserId == u.UserId);
                if (user != null)
                    if (user.AccessLevel >= UserAccessLevel.User)
                    {
                        var lastPublic = CurrentNewsForumContext.Publications.ToList().LastOrDefault();
                        int index = 1;
                        //проверяем, есть ли записи в базе
                        if (lastPublic != null)
                            index = lastPublic.PublicatoinId + 1;

                        string newPath = $@"{Server.PathServerDirecotory}\Publications\{index}\";//каталог целой публикации
                        string pathDescriptionImages = $@"{newPath}\DescriptionImages\";//каталог для фото материалов 
                        string pathMusicFiles = $@"{newPath}\DescriptionSongs\";//каталг для музыкальных файлов
                        string pathNewsPublicationFiles = $@"{newPath}\ContentNewsPublication\";//каталог для файлов статьи
                        string pathPoster = string.Empty;
                        string pathDescription = string.Empty;

                        Directory.CreateDirectory(newPath);

                        var currDescImagesPublicatiton = publication as IListBitmapImages;//проверяем, есть ли у публикации набор фотографий в описании
                        var currMusicPulication = publication as IListSongs;//проверяем, есть ли у публикации набор аудио файлов
                        
                        List<DBInfoFile> ListFilesPublication = new List<DBInfoFile>();

                        #region Локальная функция для создания и заполнения указанной директории файлами из переданной публикации

                        void CreateAndFillDirectoryFiles(string path)
                        {
                            Directory.CreateDirectory(path);
                            int nameIndex = 1;
                            currDescImagesPublicatiton.ListImages.ToList().ForEach(file =>
                            {
                                string currPath = $@"{path}{nameIndex++}{file.Type}";
                                FilesManipulation.CreateFile(file.Bytes, currPath);
                                ListFilesPublication.Add(new DBInfoFile()
                                {
                                    FullPath = currPath,
                                    Type = file.Type
                                });
                            });
                        }

                        #endregion

                        //сохраняем фотографии
                        if (currDescImagesPublicatiton != null)
                        {
                            CreateAndFillDirectoryFiles(pathDescriptionImages);
                        }
                       
                        //сохраняем аудио файлы
                        if (currMusicPulication != null)
                        {
                            CreateAndFillDirectoryFiles(pathMusicFiles);
                        }

                        //сохраняем обложку (изображение)
                        if (publication.PosterImage != null)
                        {
                            var dataPosterBytes = publication.PosterImage.Bytes;
                            pathPoster = $@"{newPath}{index}_{publication.Title}_poster{publication.PosterImage.Type}";
                            FilesManipulation.CreateFile(dataPosterBytes, pathPoster);
                        }

                        //сохраняем файл с описанием (если такой есть)
                        if (publication.Description != null)
                        {
                            var dataDescriptionArray = publication.Description.Bytes;
                            pathDescription = $@"{newPath}{index}_{publication.Title}_description.rtf";
                            FilesManipulation.CreateFile(dataDescriptionArray, pathDescription);
                        }

                        //выбираем список жанров для публикации (вытаскиваем имя и получаем список строк)
                        var genres = publication.ListGenres.Select(g => $"{g.Name}").ToList();
                        List<DBGenre> listGenres = CurrentNewsForumContext.Genres.Where(g => genres.Contains(g.Name)).ToList();

                        //DBPublication currDBPublication = new DBPublication()
                        //{
                        //    ListFiles = ListFilesPublication,
                        //    CreateDate = publication.CreateDate,
                        //    ListGenres = listGenres,
                        //    RefDescription = pathDescription,
                        //    RefPoster = pathPoster,
                        //    UserId = publication.User.UserId,
                        //    TypePublication = publication.TypePublication,
                        //    Title = publication.Title,
                        //};

                        switch (publication.TypePublication)
                        {
                            case PublicationType.Game:
                                VMGamePublication gamePublic = publication as VMGamePublication;
                                DBGamePubliaction dbGamePublic = new DBGamePubliaction()
                                {
                                    CompanyDeveloper = gamePublic.CompanyDeveloper,
                                    CreateDate = gamePublic.CreateDate,
                                    InterfaceLanguage = gamePublic.InterfaceLanguage,
                                    MultiPlayer = gamePublic.MultiPlayer,
                                    Platform = gamePublic.Platform,
                                    Title = gamePublic.Title,
                                    UserId = gamePublic.User.UserId,
                                    TypePublication = gamePublic.TypePublication,
                                    ReleaseYear = gamePublic.ReleaseYear,
                                    RefDescription = pathDescription,
                                    RefPoster = pathPoster,
                                    ListGenres = listGenres,
                                    ListFiles = ListFilesPublication
                                };
                                CurrentNewsForumContext.GamePublications.Add(dbGamePublic);
                                break;
                            case PublicationType.Music:
                                VMMusicPublication musicPublic = publication as VMMusicPublication;
                                DBMusicPublication dbMusicPublic = new DBMusicPublication()
                                {
                                    Album = musicPublic.Album,
                                    CountryPerformer = musicPublic.CountryPerformer,
                                    CreateDate = musicPublic.CreateDate,
                                    Formats = musicPublic.Formats,
                                    Performer = musicPublic.Performer,
                                    Title = musicPublic.Title,
                                    UserId = musicPublic.User.UserId,
                                    ReleaseYear = musicPublic.ReleaseYear,
                                    RefPoster = pathPoster,
                                    RefDescription = pathDescription,
                                    ListGenres = listGenres,
                                    ListFiles = ListFilesPublication,
                                };
                                CurrentNewsForumContext.MusicPublications.Add(dbMusicPublic);
                                break;
                            case PublicationType.News:
                                VMNewsPublication newsPublic = publication as VMNewsPublication;
                                List<DBNewsElement> newsElements = new List<DBNewsElement>();

                                foreach (var item in newsPublic.ListElements)
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
                                                FullPath = linkVideoEl.FullLinkForVideo,
                                                TypeElement = TypeElementOfNews.LinkVideo
                                            };
                                            break;

                                        default:
                                            break;
                                    }
                                    newsElements.Add(currDbNewsEl);
                                }

                                DBNewsPublication dbNewPublic = new DBNewsPublication()
                                {
                                    Title = newsPublic.Title,
                                    UserId = newsPublic.User.UserId,
                                    RefPoster = pathPoster,
                                    RefDescription = pathDescription,
                                    ListElements = newsElements,
                                    ListGenres = listGenres
                                };
                                CurrentNewsForumContext.NewsPublications.Add(dbNewPublic);
                                break;
                            case PublicationType.Film:
                                VMFilmPublication filmPublic = publication as VMFilmPublication;
                                DBFilmPublication dbFilmPublic = new DBFilmPublication()
                                {
                                    Director = filmPublic.Director,
                                    Country = filmPublic.Country,
                                    CreateDate = filmPublic.CreateDate,
                                    Duration = filmPublic.Duration,                                    
                                    Title = filmPublic.Title,
                                    UserId = filmPublic.User.UserId,
                                    ReleaseYear = filmPublic.ReleaseYear,
                                    RefPoster = pathPoster,
                                    RefDescription = pathDescription,
                                    ListGenres = listGenres,
                                    ListFiles = ListFilesPublication,
                                };
                                CurrentNewsForumContext.FilmPublications.Add(dbFilmPublic);
                                break;
                            default:
                                break;
                        }
                        CurrentNewsForumContext.SaveChanges();
                        result = true;
                    }
            }
            catch (Exception ex)
            {
                
            }
            return result;
        }
    }
}