using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelDataBase.VMInterfaces;
using ViewModelDataBase.VMPublicationTypes;
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

        public async static Task<StorageFile> CreateLocalStorageFile(string nameWithType)
        {
            return await Folder.CreateFileAsync(nameWithType, CreationCollisionOption.ReplaceExisting);            
        }
    }
}