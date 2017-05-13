using Model.PublicationTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using ViewModelDataBase.VMPublicationTypes;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace NewsForum.View.MyUserControls
{
    public sealed partial class PopupListUserControl : UserControl
    {

        public List<IGenre> SelectedGenres = new List<IGenre>();

        public object ItemsSource
        {
            get => GetValue(ListView.ItemsSourceProperty);
            set => SetValue(ListView.ItemsSourceProperty, GenresListView.ItemsSource = value);
        }

        public PopupListUserControl()
        {
            this.InitializeComponent();
        }

        private void GenresListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedGenres != null)
            {
                SelectedGenres.Clear();
                if (GenresListView.SelectedItems != null)
                {
                    foreach (var item in GenresListView.SelectedItems)
                    {
                        SelectedGenres.Add(new VMGenre() { Name = item as string });
                    }
                }
            }
        }
    }
}