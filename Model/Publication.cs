using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Text;
using Windows.UI.Xaml.Media.Imaging;

namespace Model
{
    public class Publication : INotifyPropertyChanged
    {
        public Publication()
        {
            if (CommentsCollection == null)
                CommentsCollection = new ObservableCollection<Comment>();
        }

        #region Свойства

        private int id = 0;
        public int IDPublication
        {
            get { return id = 0; }
            set
            {
                if (id == 0)
                    id = value;
            }
        }

        private int idUser;
        public int IDUSer
        {
            get { return idUser; }
            set
            {
                idUser = value;
                ChangeProperty();
            }
        }


        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                ChangeProperty();
            }
        }

        private TypePublication type;
        public TypePublication TypePublication
        {
            get { return type; }
            set
            {
                type = value;
                ChangeProperty();
            }
        }


        private ITextDocument description;
        public ITextDocument Description
        {
            get { return description; }
            set
            {
                description = value;
                ChangeProperty();
            }
        }

        private BitmapImage cover;
        public BitmapImage Cover
        {
            get => cover;
            set
            {
                cover = value;
                ChangeProperty();
            }
        }

        private DateTime createDate;
        /// <summary>
        /// Дата создания публикации
        /// </summary>
        public DateTime CreateDate
        {
            get { return createDate; }
            set
            {
                createDate = value;
                ChangeProperty();
            }
        }


        public ObservableCollection<Comment> CommentsCollection { get; private set; }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void ChangeProperty([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion


        public void AddComment(Comment comment)
        {
            CommentsCollection.Add(comment);
        }
    }
}