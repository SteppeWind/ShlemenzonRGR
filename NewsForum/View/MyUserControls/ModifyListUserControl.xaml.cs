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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace NewsForum.View.MyUserControls
{
    public sealed partial class ModifyListUserControl : UserControl
    {

        public event Action<IName> ItemTappedEvent;




        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(ModifyListUserControl), new PropertyMetadata(0));





        public List<IName> ItemSource
        {
            get { return (List<IName>)GetValue(ItemSourceProperty); }
            set
            {
                SetValue(ItemSourceProperty, value);
                AddRange(value);
            }
        }

        // Using a DependencyProperty as the backing store for ItemSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemSourceProperty =
            DependencyProperty.Register("ItemSource", typeof(List<IName>), typeof(ModifyListUserControl), new PropertyMetadata(0));



        public ModifyListUserControl()
        {
            this.InitializeComponent();
        }

        public void AddRange<T>(List<T> collection) where T : IName
        {
            foreach (var item in collection)
            {
                HyperlinkButton btn = new HyperlinkButton() { Content = item, Margin = new Thickness(0, 0, 10, 0) };
                btn.Tapped += Btn_Tapped;
                InfoWrapPanel.Children.Add(btn);
            }
        }

        private void Btn_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var item = (sender as HyperlinkButton).Content as IName;
            ItemTappedEvent?.Invoke(item);
        }
    }
}