using Model.PublicationTypes;
using Model.UserTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelDataBase.VMInterfaces;
using System.Runtime.CompilerServices;
using Model;
using Newtonsoft.Json;

namespace ViewModelDataBase.VMPublicationTypes
{
    public class VMComment : Comment, IModifyPropertyChanged
    {
        public User User { get; set; }

        [Property("Value")]
        public override string Value
        {
            get => base.Value;
            set
            {
                base.Value = value;
                ChangeProp();
            }
        }

        [JsonIgnore]
        public object[] Govno => new object[] { User, CommentId };

        public event PropertyChangedEventHandler PropertyChanged;

        public void ChangeProp([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public VMComment()
        {
            User = User ?? new User();
        }
    }
}