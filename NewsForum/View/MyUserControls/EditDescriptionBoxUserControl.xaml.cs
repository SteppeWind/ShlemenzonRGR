using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
        public ITextDocument Document { get; private set; }

        public String PlaceholderText
        {
            get => (string)GetValue(TextBox.PlaceholderTextProperty);
            set => SetValue(TextBox.PlaceholderTextProperty, MainRichEditBox.PlaceholderText = value);
        }

        public EditDescriptionBoxUserControl()
        { 
            this.InitializeComponent();
            Document = MainRichEditBox.Document;
        }  
    }
}