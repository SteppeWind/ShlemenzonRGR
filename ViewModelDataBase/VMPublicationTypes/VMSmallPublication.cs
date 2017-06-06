using Model.PublicationTypes;
using Model.UserTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelDataBase.VMInterfaces;
using ViewModelDataBase.VMTypes;
using System.Runtime.CompilerServices;
using Model;

namespace ViewModelDataBase.VMPublicationTypes
{
    public class VMSmallPublication : SmallPublication, IModifyPropertyChanged
    {
        [JsonIgnore]
        public override SmallPublication PublicationComponent
        {
            get => base.PublicationComponent;
            set => base.PublicationComponent = value;
        }

        [Property("IsDeleted")]
        public override bool IsDeleted
        {
            get => base.IsDeleted;
            set
            {
                base.IsDeleted = value;
                ChangeProp();
            }
        }

        [Property("IsPublished")]
        public override bool IsPublished
        {
            get => base.IsPublished;
            set
            {
                base.IsPublished = value;
                ChangeProp();
            }
        }

        public VMFile PosterImage { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void ChangeProp([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}