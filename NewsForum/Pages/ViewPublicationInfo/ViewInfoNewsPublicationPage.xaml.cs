using Model.PublicationTypes.NewsPublications;
using NewsForum.Model;
using NewsForum.View.Converters;
using NewsForum.View.MyUserControls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using ViewModelDataBase.VMPublicationTypes.VMNewsTypes;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace NewsForum.Pages.ViewPublicationInfo
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class ViewInfoNewsPublicationPage : Page
    {
        VMNewsPublication Publication { get; set; }

        public ViewInfoNewsPublicationPage()
        {
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Publication = e.Parameter as VMNewsPublication;
            a();
        }

        async void a()
        {
            await Task.Delay(100);
            this.InitializeComponent();

            foreach (var item in Publication.ListElements)
            {
                switch (item.TypeElement)
                {
                    case TypeElementOfNews.LinkVideo:
                        ModifyWebViewUserControl wv = new ModifyWebViewUserControl()
                        {
                            Margin = new Thickness(0, 0, 0, 15)
                        };
                        wv.HtmlCode = (item as VMElementLinkVideo).HTMLCode;
                        EditorContentSP.Children.Add(wv);
                        break;
                    case TypeElementOfNews.Separator:
                        EditorContentSP.Children.Add(new SeparatorUserControl() { Margin = new Thickness(0, 0, 0, 15) });
                        break;
                    case TypeElementOfNews.Image:
                        Image img = new Image()
                        {
                            Source = new FileConverter()
                            .Convert((item as VMNewsElementFile)
                            .FullPath, null, null, null) as BitmapImage,
                            Margin = new Thickness(0, 0, 0, 15)
                        };
                        EditorContentSP.Children.Add(img);
                        break;
                    case TypeElementOfNews.Text:
                        var box = new EditDescriptionBoxUserControl()
                        {
                            IsEditBox = false,
                            Margin = new Thickness(0, 0, 0, 15)
                        };
                        await box.LoadDocumentsStreamToBox(await StorageFile.GetFileFromPathAsync((item as VMNewsElementFile).FullPath));
                        EditorContentSP.Children.Add(box);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}