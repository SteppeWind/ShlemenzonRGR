using Model.PublicationTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace NewsForum.Pages
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class ExpandSearchPage : Page
    {
        
        public ExpandSearchPage()
        {
            this.InitializeComponent();
            GenresListView.ItemsSource = GenreTypes.GameGenres;
        }

        private void GenresCombox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GenresListView != null)
                switch (GenresCombox.SelectedIndex)
                {
                    case 0:
                        GenresListView.ItemsSource = GenreTypes.GameGenres;
                        break;

                    case 1:
                        GenresListView.ItemsSource = GenreTypes.FilmGenres;
                        break;

                    case 2:
                        GenresListView.ItemsSource = GenreTypes.MusicGenres;
                        break;
                    default:
                        break;
                }
        }
    }
}