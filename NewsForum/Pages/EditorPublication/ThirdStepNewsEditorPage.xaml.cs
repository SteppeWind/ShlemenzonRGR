using NewsForum.View.MyUserControls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using ViewModelDataBase.VMPublicationTypes.VMNewsTypes;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using Model.PublicationTypes.NewsPublications;
using NewsForum.Model;
using Model.PublicationTypes;
using ViewModelDataBase.VMPublicationTypes;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace NewsForum.Pages.EditorPublication
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class ThirdStepNewsEditorPage : Page
    {
        public List<NewsElement> ListElements { get; private set; } = new List<NewsElement>();
        public VMNewsPublication Publication { get; private set; }
        
        public ThirdStepNewsEditorPage()
        {
            this.InitializeComponent();
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Publication = e.Parameter as VMNewsPublication;
        }

        protected async override void OnNavigatedFrom(NavigationEventArgs e)
        {
            foreach (var item in EditorContentListView.Children)
            {
                var currItem = (item as ContainerForUserControl).ContentUserControl;
                switch (currItem.Tag)
                {
                    case "CoverImage":

                        var file = (currItem as AddCoverPublicationUserControl).ImageFile;
                        var similarfile = await FilesAction.CreateLocalStorageFile(Guid.NewGuid().ToString() + file.Type, file.Bytes);
                        ListElements.Add(new VMNewsElementFile()
                        {
                            FullPath = similarfile.Path,
                            Bytes = file.Bytes,
                            NumberOfList = ListElements.Count + 1,
                            Type = file.Type,
                            TypeElement = TypeElementOfNews.Image
                        });
                        break;

                    case "DescriptionBox":
                        var descFile = await FilesAction.CreateLocalStorageFile($"{ListElements.Count + 1}.rtf");
                        await (currItem as EditDescriptionBoxUserControl).SaveDocumentStreamToFile(descFile);
                        file = (currItem as EditDescriptionBoxUserControl).DescriptionFile;
                        ListElements.Add(new VMNewsElementFile()
                        {
                            FullPath = file.FullPath,
                            Bytes = file.Bytes,
                            NumberOfList = ListElements.Count + 1,
                            Type = file.Type,
                            TypeElement = TypeElementOfNews.Text
                        });
                        break;

                    case "Separator":
                        ListElements.Add(new NewsElement()
                        {
                            NumberOfList = ListElements.Count + 1
                        });
                        break;

                    case "LinkVideo":
                        var linkEl = (currItem as LinkVideoViewUserControl).LinkVideoElement;
                        linkEl.NumberOfList = ListElements.Count + 1;
                        ListElements.Add(linkEl);
                        break;
                    default:
                        break;
                }
            }
            Publication.ListElements = ListElements;
            Publication.ListGenres = GenresListView.SelectedItems.Select(genre => new Genre() { Name = (genre as Genre).Name }).ToList();
        }

        private void PanelAddElementsToPublicGriView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var obj = e.ClickedItem as TextBlock;
            switch (obj.Tag)
            {
                case "Image":
                    var cover = new AddCoverPublicationUserControl();
                    AddControlToEditPanel(cover);
                    break;

                case "Text":
                    var box = new EditDescriptionBoxUserControl();
                    PanelEditDecriptionPublicationUserControl.AddEditDescriptionBox(box);
                    AddControlToEditPanel(box);
                    break;

                case "Separator":
                    AddControlToEditPanel(new SeparatorUserControl());
                    break;

                case "Video":
                    AddControlToEditPanel(new LinkVideoViewUserControl());
                    break;
                default:
                    break;
            }
        }

        private void AddControlToEditPanel(UserControl currentControl)
        {
            ContainerForUserControl cfuc = new ContainerForUserControl();
            cfuc.AddContent(currentControl);
            cfuc.DeleteStartingEvent += ContentUserControl_DeleteStartingEvent;
            EditorContentListView.Children.Add(cfuc);
        }

        private void ContentUserControl_DeleteStartingEvent(object sender, TappedRoutedEventArgs e)
        {
            EditorContentListView.Children.Remove((UIElement)sender);
            if (sender.GetType() == typeof(EditDescriptionBoxUserControl))
            {
                PanelEditDecriptionPublicationUserControl.RemoveEditDescriptionBox((EditDescriptionBoxUserControl)sender);
            }
        }
    }
}