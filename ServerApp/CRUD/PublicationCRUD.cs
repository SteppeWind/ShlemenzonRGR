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

namespace ServerApp.CRUD
{
    public static class PublicationCRUD
    {
        private static NewsForumContext CurrentNewsForumContext = NewsForumContext.GetNewsForumContext;

        public static bool CreatePublication(VMPublication publication)
        {
            // TextGetOptions tgo = TextGetOptions.FormatRtf;
            bool result = false;
            var pbl = publication as VMGamePublication;
            var user = CurrentNewsForumContext.Users.FirstOrDefault(u => publication.User.UserId == u.UserId);
            if (user != null)
                if (user.AccessLevel >= UserAccessLevel.User)
                {
                    var lastPublic = CurrentNewsForumContext.Publications.Last();
                    int index = 1;
                    //проверяем, есть ли записи в базе
                    if (lastPublic != null)
                        index = lastPublic.PublicatoinId + 1;

                    string newPath = $@"{Server.PathServerDirecotory}\Publications\{index}\";//каталог целой публикации
                    string pathDescriptionImages = $@"{newPath}\DescriptionImages\";//каталог для фото материалов 
                    string pathMusicFiles = $@"{newPath}\DescriptionSongs\";//каталг для музыкальных файлов
                    string pathPoster = string.Empty;
                    string pathDescription = string.Empty;

                    Directory.CreateDirectory(newPath);
                    var currPublicatiton = publication as IListBitmapImages;//проверяем, есть ли у публикации набор фотографий в описании
                    List<DBInfoFile> ListFilesPublication = new List<DBInfoFile>();
                    if (currPublicatiton != null)
                    {
                        int nameIndex = 1;
                        currPublicatiton.ListImages.ToList().ForEach(file =>
                        {
                            string currPath = $@"{pathDescriptionImages}{nameIndex++}.{file.Type}";
                            FilesManipulation.CreateFile(file.Bytes, currPath);
                            ListFilesPublication.Add(new DBInfoFile()
                            {
                                FullPath = currPath,
                                PublicatoinId = index
                            });
                        });
                    }
                    //сохраняем обложку (изображение)
                    if (publication.PosterImage != null)
                    {
                        var dataPosterBytes = publication.PosterImage.Bytes;
                        pathPoster = $@"{newPath}{index}_{publication.Title}_poster.{publication.PosterImage.Type}";
                        FilesManipulation.CreateFile(dataPosterBytes, pathPoster);
                    }
                    //сохраняем файл с описанием (если такой есть)
                    if (publication.Description != null)
                    {
                        var dataDescriptionArray = publication.Description.Bytes;
                        pathDescription = $@"{newPath}{index}_{publication.Title}_description.rtf";
                        FilesManipulation.CreateFile(dataDescriptionArray, pathDescription);
                    }

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
                                ListGenres = gamePublic.ListGenres.Select(c => new DBGenre()
                                {
                                    Name = c.Name,
                                    // GentreId = CurrentNewsForumContext.Genres.FirstOrDefault(genre => genre.Name == c.Name).GentreId
                                }).ToList(),
                                ListFiles = ListFilesPublication,
                                PublicatoinId = index
                            };

                            CurrentNewsForumContext.GamePublications.Add(dbGamePublic);
                            CurrentNewsForumContext.SaveChanges();
                            result = true;
                            break;
                        case PublicationType.Music:
                            VMMusicPublication musicPublic = publication as VMMusicPublication;
                            break;
                        case PublicationType.News:
                            VMNewsPublication newsPublic = publication as VMNewsPublication;
                            break;
                        case PublicationType.Film:
                            VMFilmPublication filmPublic = publication as VMFilmPublication;
                            break;
                        default:
                            break;
                    }
                }

            return result;
        }
    }
}