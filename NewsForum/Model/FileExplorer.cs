using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace NewsForum.Model
{
    class FileExplorer
    {
        public static readonly string[] FiltersImage = { "jpg", "png", "jpeg" };
        public static readonly string[] FiltersMusic = { "wav", "mp3" };
        public static readonly string[] ContentImageTypes = { "image/png", "image/jpeg" };
        public static readonly string[] ContentMusicTypes = { "audio/mpeg" };


        private FileOpenPicker OpenPicker;

        public event Action LoadStrat;
        public event Action<IEnumerable<StorageFile>> LoadEnded;
        
        public FileExplorer(PickerLocationId location)
        {
            OpenPicker = new FileOpenPicker()
            {
                CommitButtonText = "Открыть",
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = location
            };
        }

        public void SetFilterType(params string[] types)
        {
            foreach (var item in types)
            {
                OpenPicker.FileTypeFilter.Add($".{item}");
            }
        }

        public async Task<IReadOnlyList<StorageFile>> PickMultipleFilesAsync()
        {
            LoadStrat?.Invoke();
            var result = await OpenPicker.PickMultipleFilesAsync();
            LoadEnded?.Invoke(result);
            return result;
        }

        public async Task<StorageFile> PickSingleFileAsync()
        {
            LoadStrat.Invoke();
            var result = await OpenPicker.PickSingleFileAsync();
            LoadEnded?.Invoke(new List<StorageFile>() { result });
            return result;
        }
    }
}