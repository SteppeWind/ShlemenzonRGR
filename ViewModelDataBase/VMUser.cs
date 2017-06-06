using Model.PublicationTypes;
using Model.UserTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelDataBase.VMInterfaces;
using ViewModelDataBase.VMPublicationTypes;
using System.Runtime.CompilerServices;
using Model;

namespace ViewModelDataBase
{
    public class VMUser : User, IUser<VMPublication, Rating, Comment>, IModifyPropertyChanged
    {
        //[JsonIgnore]
        //public override User UserComponent { get => base.UserComponent; set => base.UserComponent = value; }

        [Property("AccessLevel")]
        public override UserAccessLevel AccessLevel
        {
            get => base.AccessLevel;
            set
            {
                base.AccessLevel = value;
                ChangeProp();
            }
        }

        [Property("IsBunned")]
        public override bool IsBunned
        {
            get => base.IsBunned;
            set
            {
                base.IsBunned = value;
                ChangeProp();
            }
        }
        [JsonIgnore]
        public override bool IsCorrectUserForAutorize => base.IsCorrectUserForAutorize;

        public List<VMPublication> ListPublications { get; set; }

        public List<Rating> ListRatings { get; set; }

        public List<Comment> ListComments { get; set; }

        public VMUser()
        {
            ListRatings = ListRatings ?? new List<Rating>();
            ListComments = ListComments ?? new List<Comment>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void ChangeProp([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}