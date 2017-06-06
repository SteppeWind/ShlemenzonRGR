using NewsForum.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using ViewModelDataBase.VMTypes;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Text;
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
    public sealed partial class EditDescriptionBoxUserControl : UserControl
    {
        public VMFile DescriptionFile { get; private set; }

        public ITextDocument Document { get; private set; }

        public String PlaceholderText
        {
            get => (string)GetValue(TextBox.PlaceholderTextProperty);
            set => SetValue(TextBox.PlaceholderTextProperty, MainRichEditBox.PlaceholderText = value);
        }

        public static readonly DependencyProperty IsEditBoxProperty = DependencyProperty.Register("IsEditBox",
                                                                                  typeof(bool),
                                                                                  typeof(EditDescriptionBoxUserControl),
                                                                                  new PropertyMetadata(false));

        public bool IsEditBox
        {
            get => (bool)GetValue(IsEditBoxProperty);
            set
            {
                SetValue(IsEditBoxProperty, value);
                if (!value)
                {
                    MainRichEditBox.Style = App.Current.Resources["RichEditBoxStyle"] as Style;
                }
                else
                {
                    MainRichEditBox.Style = App.Current.Resources["BaseRichEditBoxStyle"] as Style;
                }
            }
        }

        public async Task SaveDocumentStreamToFile(StorageFile file)
        {
            using (var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                Document.SaveToStream(Windows.UI.Text.TextGetOptions.FormatRtf, stream);
            }
            var result = await FilesAction.ConvertToIFileVM(file);
            DescriptionFile = new ViewModelDataBase.VMTypes.VMFile()
            {
                FullPath = file.Path,
                Name = file.DisplayName,
                Bytes = result.Item2,
                Type = result.Item1
            };
        }

        public async Task LoadDocumentsStreamToBox(StorageFile file)
        {
            using (var stream = await file.OpenReadAsync())
            {
                Document.LoadFromStream(TextSetOptions.FormatRtf, stream);
            }
        }

        public EditDescriptionBoxUserControl()
        { 
            this.InitializeComponent();
            Document = MainRichEditBox.Document;
        }  
    }
}