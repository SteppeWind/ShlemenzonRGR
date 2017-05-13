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
using ViewModelDataBase.VMTypes;

namespace ViewModelDataBase.VMPublicationTypes
{
    public class VMPublication : Publication
    {
        public VMDescription Description { get; set; }

        public VMImage PosterImage { get; set; }

        public VMUser User { get; set; }
        
        public ObservableCollection<VMRating> ListMarks { get; set; }

        public ObservableCollection<VMComment> ListComments { get; set; }
    }
}