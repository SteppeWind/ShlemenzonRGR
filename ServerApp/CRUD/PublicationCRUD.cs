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
using ViewModelDataBase.VMTypes;
using RequestServer.PublicationsRequest;
using ServerApp.Converting;
using RequestServer.Request;

namespace ServerApp.CRUD
{
    public static class PublicationCRUD
    {
        private static NewsForumContext CurrentNewsForumContext = NewsForumContext.GetNewsForumContext;

        public static List<DBPublication> GetPublishedPublications => CurrentNewsForumContext.Publications.Where(p => p.IsPublished == true).ToList();

        public static List<DBPublication> GetDeletedPublications => CurrentNewsForumContext.Publications.Where(p => p.IsDeleted).ToList();

        public static bool CreatePublication(VMPublication publication)
        {
            bool result = false;
            try
            {
                var user = UserCRUD.GetDBUserFromId(publication.UserId);
                if (user != null && user.AccessLevel >= UserAccessLevel.User)
                {
                    var lastPublic = CurrentNewsForumContext.Publications.ToList().LastOrDefault();
                    int index = 1;
                    //проверяем, есть ли записи в базе
                    if (lastPublic != null)
                        index = lastPublic.PublicationId + 1;

                    DBPublication dbPublication = new DBPublication() { PublicationId = index };

                    CreateFilesForPublication(publication, ref dbPublication, true);
                    
                    CurrentNewsForumContext.Publications.Add(dbPublication);
                    CurrentNewsForumContext.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public static VMPublication GetPublication(int publicationId)
        {
            VMPublication result = null;
            DBPublication dbPublication = CurrentNewsForumContext.Publications.FirstOrDefault(p => p.PublicationId == publicationId);

            if (dbPublication != null)
            {
                switch (dbPublication.TypePublication)
                {
                    case PublicationType.Game:
                        result = new PublicationConverter<DBPublication, VMGamePublication>().ConvertPublication(dbPublication);
                        break;
                    case PublicationType.Music:
                        result = new PublicationConverter<DBPublication, VMMusicPublication>().ConvertPublication(dbPublication);
                        break;

                    case PublicationType.Film:
                        result = new FilmPublicationConverter().ConvertPublication(dbPublication as DBFilmPublication);
                        break;

                    case PublicationType.News:
                        result = new NewsPublicationConvert().ConvertPublication(dbPublication as DBNewsPublication);
                        break;
                }
            }

            return result;
        }

        private static void CreateFilesForPublication(VMPublication publication, ref DBPublication dbPublication, bool isCreatePublication = false)
        {
            int index = dbPublication.PublicationId;
            
            string publicationTitleForDirectory = new string(publication.Title
                .Where(c => !FilesManipulation.ForbiddenSymbols.Contains(c))
                .Select(c => c).ToArray());

            string newPath = $@"{Server.PathServerDirecotory}\Publications\{index}\";//каталог целой публикации
            string pathDescriptionImages = $@"{newPath}\DescriptionImages\";//каталог для фото материалов 
            string pathMusicFiles = $@"{newPath}\DescriptionSongs\";//каталoг для музыкальных файлов
            string pathNewsPublicationFiles = $@"{newPath}\ContentNewsPublication\";//каталог для файлов статьи
            string pathPoster = string.Empty;
            string pathDescription = string.Empty;

            Directory.CreateDirectory(newPath);

            var currDescImagesPublicatiton = publication as IListBitmapImages;//проверяем, есть ли у публикации набор фотографий в описании
            var currMusicPulication = publication as IListSongs;//проверяем, есть ли у публикации набор аудио файлов

            List<DBInfoFile> ListFilesPublication = new List<DBInfoFile>();

            #region Локальная функция для создания и заполнения указанной директории файлами из переданной публикации

            void CreateAndFillDirectoryFiles(string path, List<VMFile> files)
            {
                Directory.CreateDirectory(path);
                int nameIndex = 1;
                files.ToList().ForEach(file =>
                {
                    string currPath = $@"{path}{nameIndex++}{file.Type}";
                    FilesManipulation.CreateFile(file.Bytes, currPath);
                    ListFilesPublication.Add(new DBInfoFile()
                    {
                        Name = file.Name,
                        FullPath = currPath,
                        Type = file.Type
                    });
                });
            }

            #endregion

            //сохраняем фотографии
            if (currDescImagesPublicatiton != null)
            {
                CreateAndFillDirectoryFiles(pathDescriptionImages, currDescImagesPublicatiton.ListImages);
            }

            //сохраняем аудио файлы
            if (currMusicPulication != null)
            {
                CreateAndFillDirectoryFiles(pathMusicFiles, currMusicPulication.ListSongs);
            }


            //сохраняем обложку (изображение)
            if (publication.PosterImage != null)
            {
                var dataPosterBytes = publication.PosterImage.Bytes;
                pathPoster = $@"{newPath}{index}_{publicationTitleForDirectory}_poster{publication.PosterImage.Type}";
                FilesManipulation.CreateFile(dataPosterBytes, pathPoster);
            }

            //сохраняем файл с описанием (если такой есть)
            if (publication.Description != null)
            {
                var dataDescriptionArray = publication.Description.Bytes;
                pathDescription = $@"{newPath}{index}_{publicationTitleForDirectory}_description.rtf";
                FilesManipulation.CreateFile(dataDescriptionArray, pathDescription);
            }

            if (isCreatePublication)
            {
                switch (publication.TypePublication)
                {
                    case PublicationType.Game:
                        dbPublication = new PublicationConverter<DBGamePubliaction, VMPublication>().ConvertPublication(publication);
                        break;
                    case PublicationType.Music:
                        dbPublication = new PublicationConverter<DBMusicPublication, VMPublication>().ConvertPublication(publication);
                        break;
                    case PublicationType.News:
                        Directory.CreateDirectory(pathNewsPublicationFiles);
                        VMNewsPublication newsPublic = publication as VMNewsPublication;
                        dbPublication = new NewsPublicationConvert().ConvertAndCreatePublication(pathNewsPublicationFiles, newsPublic);
                        break;
                    case PublicationType.Film:
                        dbPublication = new FilmPublicationConverter().ConvertPublication(publication as VMFilmPublication);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                dbPublication.ListGenres = CurrentNewsForumContext.Genres
                    .Where(dbGenre => publication.ListGenres
                        .Select(genre => genre.Name)
                        .Contains(dbGenre.Name))
                    .ToList();
                dbPublication.Convert(publication);
                switch (publication.TypePublication)
                {
                    case PublicationType.News:
                        Directory.CreateDirectory(pathNewsPublicationFiles);
                        VMNewsPublication newsPublic = publication as VMNewsPublication;
                        DBNewsPublication dbNewsPublic = dbPublication as DBNewsPublication;
                        NewsPublicationConvert.CreateAndSaveListElements(ref dbNewsPublic, newsPublic, pathNewsPublicationFiles);
                        break;
                    case PublicationType.Film:
                        DBFilmPublication dbFilm = dbPublication as DBFilmPublication;
                        FilmPublicationConverter.SaveActors(ref dbFilm, publication as VMFilmPublication);
                        break;
                    default:
                        break;
                }
            }


            dbPublication.ListFiles = ListFilesPublication;
            if (File.Exists(pathPoster))
            {
                dbPublication.RefPoster = pathPoster;
            }

            if (File.Exists(pathDescription))
            {
                dbPublication.RefDescription = pathDescription;
            }
        }

        public static bool UpdatePublication(VMPublication publication, int userId)
        {
            var dbPublication = CurrentNewsForumContext.Publications.FirstOrDefault(p => p.PublicationId == publication.PublicationId);
            var dbUser = UserCRUD.GetDBUserFromId(userId);

            if (dbUser != null)
            {
                var findedPubl = dbUser.ListPublications.FirstOrDefault(p => p.PublicationId == publication.PublicationId);
                if (findedPubl == null)
                {
                    return false;
                }
                
                if ((dbPublication != null && dbUser.AccessLevel >= UserAccessLevel.User) || dbUser.AccessLevel >= UserAccessLevel.Admin)
                {
                    Directory.Delete($@"{Server.PathServerDirecotory}\Publications\{dbPublication.PublicationId}", true);
                    CreateFilesForPublication(publication, ref dbPublication);
                    return true;
                }
            }
            return false;
        }

        public static bool DeletePublication(int idPublication, int idUser)
        {
            var currUser = UserCRUD.GetDBUserFromId(idUser);
            var currPublication = CurrentNewsForumContext.Publications.FirstOrDefault(p => p.PublicationId == idPublication);
            if (currUser != null && currPublication != null)
            {
                if (currUser.AccessLevel >= UserAccessLevel.Admin)
                {
                    currPublication.IsDeleted = true;
                    CurrentNewsForumContext.SaveChanges();
                    return true;
                }

                if (currUser.ListPublications.FirstOrDefault(p => p.PublicationId == idPublication) != null)
                {
                    currPublication.IsDeleted = true;
                    CurrentNewsForumContext.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public static bool UndeletePublication(int idPublication, int idUser)
        {
            var currUser = UserCRUD.GetDBUserFromId(idUser);
            var currPublication = CurrentNewsForumContext.Publications.FirstOrDefault(p => p.PublicationId == idPublication);
            if (currUser != null && currPublication != null)
            {
                if (currUser.AccessLevel >= UserAccessLevel.Admin)
                {
                    currPublication.IsDeleted = false;
                    CurrentNewsForumContext.SaveChanges();
                    return true;
                }

                if (currUser.ListPublications.FirstOrDefault(p => p.PublicationId == idPublication) != null)
                {
                    currPublication.IsDeleted = false;
                    CurrentNewsForumContext.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public static List<VMSmallPublication> GetSmallPublications(ReadPublciationRequest request, int userId)
        {
            var currUser = UserCRUD.GetDBUserFromId(userId);
            var result = new List<VMSmallPublication>();
            DateTime left = DateTime.MinValue;
            DateTime right = DateTime.MaxValue;

            if (request.LeftLimitTime != null && request.RightLimitTime != null)
            {
                left = DateTime.Parse(request.LeftLimitTime);
                right = DateTime.Parse(request.RightLimitTime);
            }

            var searchList = new List<DBPublication>();

            if (currUser != null && currUser.AccessLevel >= UserAccessLevel.Admin)
            {
                searchList = CurrentNewsForumContext.Publications.ToList();
            }

            if (currUser == null || currUser.AccessLevel <= UserAccessLevel.User)
            {
                searchList = GetPublishedPublications;
            }

            if (request.ListGenres.Any())
            {
                searchList = searchList
                    .Where(p => p.ListGenres
                        .Select(g => g.Name)
                        .Intersect(request.ListGenres)
                        .Any())
                    .ToList();
            }

            if (request.PublicationType == PublicationType.Any)
            {
                foreach (var p in searchList)
                {
                    result.Add(ConvertToBasePublication.GetBasePublication(p));
                }
                return result;
            }

            searchList = searchList
                .Where(p => p.CreateDate >= left && p.CreateDate <= right)
                .Where(p => p.TypePublication == request.PublicationType)
                .ToList();

            foreach (var p in searchList)
            {
                result.Add(ConvertToBasePublication.GetBasePublication(p));
            }

            return result;
        }

        public static List<VMSmallPublication> GetCertainPublications(int userId)
        {
            return CurrentNewsForumContext.Publications
                .ToList()
                .Where(p => p.UserId == userId)
                .Select(p => ConvertToBasePublication.GetBasePublication(p))
                .ToList();
        }
    }
}