using Model.PublicationTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelDataBase.VMInterfaces;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Text;
using System.Collections.ObjectModel;

namespace ViewModelDataBase.VMPublicationTypes
{
    public class VMPublication : Publication, IBitmapImage, IDescriptionVM
    {
        public BitmapImage Poster { get; set; }

        public ITextDocument Description { get; set; }

        public VMUser User { get; set; }

        public ObservableCollection<VMRating> ListMarks { get; set; }

        public ObservableCollection<VMComment> ListComments { get; set; }
    }
}