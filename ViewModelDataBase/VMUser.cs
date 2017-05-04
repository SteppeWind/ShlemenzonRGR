using Model.UserTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelDataBase.VMPublicationTypes;

namespace ViewModelDataBase
{
    public class VMUser : User
    {
        public ObservableCollection<VMRating> ListMarks { get; set; }

        public ObservableCollection<VMComment> ListComments { get; set; }

        public ObservableCollection<VMPublication> ListPublication { get; set; }
    }
}