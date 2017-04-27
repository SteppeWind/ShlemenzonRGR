using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class PanelEditDecriptionPublicationUserControl : UserControl
    {
        public event ItemClickEventHandler ItemClick;
        public List<EditDescriptionBoxUserControl> EditDescriptionBoxsList { get; private set; }

        public object Header
        {
            get => GetValue(ListViewBase.HeaderProperty);
            set => SetValue(ListViewBase.HeaderProperty, PanelEditDecriptionGridView.Header = value);
        }

        private EditDescriptionBoxUserControl CurrentFocusedControl;


        public PanelEditDecriptionPublicationUserControl()
        {
            this.InitializeComponent();
            EditDescriptionBoxsList = new List<EditDescriptionBoxUserControl>();
        }

        private void PanelEditDecriptionGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var obj = e.ClickedItem as FrameworkElement;
            if (CurrentFocusedControl != null)
            {
                ITextSelection selectedText = CurrentFocusedControl.Document.Selection;
                ITextCharacterFormat format = selectedText.CharacterFormat;
                ParagraphAlignment aligment = ParagraphAlignment.Undefined;
                switch (obj.Tag)
                {
                    case "Italic":
                        format.Italic = FormatEffect.Toggle;
                        break;

                    case "Bold":
                        format.Bold = FormatEffect.Toggle;
                        break;

                    case "Underline":
                        if (format.Underline == UnderlineType.None)
                        {
                            format.Underline = UnderlineType.Single;
                        }
                        else
                        {
                            format.Underline = UnderlineType.None;
                        }
                        break;

                    case "LeftAlign":
                        aligment = ParagraphAlignment.Left;
                        break;

                    case "CenterAlign":
                        aligment = ParagraphAlignment.Center;
                        
                        break;

                    case "RightAlign":
                        aligment = ParagraphAlignment.Right;
                        break;
                    default:
                        break;
                }
                selectedText.CharacterFormat = format;
                selectedText.ParagraphFormat.Alignment = aligment;
            }
            ItemClick?.Invoke(sender, e);
        }

        private void AddLinkToTextButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            LinkPanelFlyout.ShowAt((FrameworkElement)sender);
        }

        public void AddEditDescriptionBox(EditDescriptionBoxUserControl control)
        {
            EditDescriptionBoxsList.Add(control);
            control.GotFocus += Control_GotFocus;
        }

        private void Control_GotFocus(object sender, RoutedEventArgs e) => CurrentFocusedControl = sender as EditDescriptionBoxUserControl;

        public void RemoveEditDescriptionBox(EditDescriptionBoxUserControl control)
        {
            EditDescriptionBoxsList.Remove(control);
            control.GotFocus -= Control_GotFocus;
        }
        private void FontSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CurrentFocusedControl != null)
            {
                ITextSelection selectedText = CurrentFocusedControl.Document.Selection;
                ITextCharacterFormat format = selectedText.CharacterFormat;
                format.Size = Convert.ToSingle(FontSizeComboBox.SelectedItem);
                selectedText.CharacterFormat = format;
            }
        }
    }
}