using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelDataBase.VMInterfaces;
using ViewModelDataBase.VMPublicationTypes;
using ViewModelDataBase.VMPublicationTypes.VMNewsTypes;
using ViewModelDataBase.VMTypes;
using Windows.Storage;

namespace NewsForum.Model
{
    class FilesAction
    {
        public static StorageFolder Folder => ApplicationData.Current.LocalFolder;

        public async static Task<Tuple<string, byte[]>> ConvertToIFileVM(StorageFile storeFile)
        {
            Tuple<string, byte[]> result;
            using (Stream stream = await storeFile.OpenStreamForReadAsync())
            {
                BinaryReader br = new BinaryReader(stream);
                var bytes = br.ReadBytes((int)stream.Length);
                result = new Tuple<string, byte[]>(storeFile.FileType, bytes);
            }
            return result;
        }

        public async static Task<VMFile> ConvertToIFileVM(StorageFile storeFile, bool check)
        {
            VMFile result;
            using (Stream stream = await storeFile.OpenStreamForReadAsync())
            {
                BinaryReader br = new BinaryReader(stream);
                var bytes = br.ReadBytes((int)stream.Length);
                result = new VMFile()
                {
                    Bytes = bytes,
                    Type = storeFile.FileType,
                    FullPath = storeFile.Path,
                    Name = storeFile.DisplayName
                };
            }
            return result;
        }

        public async static Task<StorageFile> CreateLocalStorageFile(string nameWithType)
        {
            return await Folder.CreateFileAsync(nameWithType, CreationCollisionOption.ReplaceExisting);
        }

        public async static Task<StorageFile> CreateLocalStorageFile(string nameWithType, byte[] data)
        {
            var file = await Folder.CreateFileAsync(nameWithType, CreationCollisionOption.ReplaceExisting);
            using (var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                await stream.AsStreamForWrite().WriteAsync(data, 0, data.Length);
            }
            return file;
        }

        public async static Task CreateFilesPublication(VMPublication publication)
        {
            if (publication != null)
            {
                if (publication.PosterImage != null)
                {
                    var file = await CreateLocalStorageFile(Guid.NewGuid().ToString() + publication.PosterImage.Type, publication.PosterImage.Bytes);
                    publication.PosterImage.FullPath = file.Path;
                }

                if (publication.Description != null)
                {
                    var file = await CreateLocalStorageFile("Description" + publication.Description.Type, publication.Description.Bytes);
                    publication.Description.FullPath = file.Path;
                }

                foreach (var item in publication.ListFiles)
                {
                    var file = await CreateLocalStorageFile(Guid.NewGuid().ToString() + item.Type, item.Bytes);
                    item.FullPath = file.Path;
                }
            }
        }

        public async static Task CreateFilesPublication(VMNewsPublication publication)
        {
            if (publication != null)
            {
                if (publication.PosterImage != null)
                {
                    var file = await CreateLocalStorageFile(Guid.NewGuid().ToString() + publication.PosterImage.Type, publication.PosterImage.Bytes);
                    publication.PosterImage.FullPath = file.Path;
                }

                foreach (var item in publication.ListElements)
                {
                    if (item is VMNewsElementFile currFile)
                    {
                        var file = await CreateLocalStorageFile(Guid.NewGuid().ToString() + currFile.Type, currFile.Bytes);
                        currFile.FullPath = file.Path;
                    }
                    if (item is VMElementLinkVideo linkEl)
                    {
                        linkEl.SetResultCode();
                    }
                }
            }
        }


        public async static Task CreatePostersPublications(List<VMSmallPublication> listPublications)
        {
            foreach (var item in listPublications)
            {
                var name = item.PublicationId + item.PosterImage.Type;
                item.PosterImage.FullPath = Folder.Path + $"\\{name}";
                await CreateLocalStorageFile(name, item.PosterImage.Bytes);
            }
        }
    }
}