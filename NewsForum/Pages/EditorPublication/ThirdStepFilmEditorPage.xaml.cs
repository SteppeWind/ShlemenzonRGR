using NewsForum.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace NewsForum.Pages.EditorPublication
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class ThirdStepDistributionEditorPage : Page
    {
        
        public ThirdStepDistributionEditorPage()
        {
            this.InitializeComponent();
            EditPanelPublication.AddEditDescriptionBox(EditDescriptionBox);
        }

        private void AddActorButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
        }

        private void DeleteActorsButton_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }
    }
}