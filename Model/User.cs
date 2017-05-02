using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NewsForum.Model
{
    public class User : INotifyPropertyChanged
    {

        private int id = -1;
        public int ID
        {
            get { return id; }
            set
            {
                if (id == -1)
                    id = value;
            }
        }


        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                ChangeProperty();
            }
        }

        private string surname;
        public string Surname
        {
            get { return surname; }
            set
            {
                surname = value;
                ChangeProperty();
            }
        }

        private string city;
        public string City
        {
            get { return city; }
            set
            {
                city = value;
                ChangeProperty();
            }
        }

        private string phoneNumber;
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                phoneNumber = value;
                ChangeProperty();
            }
        }

        private UserAccessLevel accessLevel = UserAccessLevel.Goest;
        public UserAccessLevel AccessLevel
        {
            get { return accessLevel; }
            set
            {
                accessLevel = value;
                ChangeProperty();
            }
        }

        private ObservableCollection<Publication> listPublications;
        public ObservableCollection<Publication> ListPublications
        {
            get { return listPublications; }
            set
            {
                listPublications = listPublications ?? value;
            }
        }

        private ICollection<Rating> listMarks;
        /// <summary>
        /// Общий список оценок публикации
        /// </summary>
        public ICollection<Rating> ListMarks
        {
            get { return listMarks; }
            set { listMarks = listMarks ?? value; }
        }

        public ICollection<Comment> CommentsCollection { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void ChangeProperty([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}