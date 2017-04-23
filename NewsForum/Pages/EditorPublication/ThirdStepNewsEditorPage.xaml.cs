using NewsForum.View.MyUserControls;
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
using Windows.UI.Xaml.Shapes;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace NewsForum.Pages.EditorPublication
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class ThirdStepNewsEditorPage : Page
    {
        public ThirdStepNewsEditorPage()
        {
            this.InitializeComponent();
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
        }

        private void PanelAddElementsToPublicGriView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var obj = e.ClickedItem as TextBlock;
            switch (obj.Tag)
            {
                case "Image":
                    AddControlToEditPanel(new AddCoverPublicationUserControl());
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

        private void MyUserControl_DeleteStartingEvent(object sender, TappedRoutedEventArgs e)
        {
            EditorContentListView.Children.Remove((UIElement)sender);
        }
    }
}