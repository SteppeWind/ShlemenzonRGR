using Model.PublicationTypes;
using NewsForum.Model;
using NewsForum.Pages;
using Newtonsoft.Json;
using RequestServer.PublicationsRequest;
using RequestServer.Request;
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


        public Frame CurrentFrame
        {
            get { return (Frame)GetValue(CurrentFrameProperty); }
            set { SetValue(CurrentFrameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentFrame.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentFrameProperty =
            DependencyProperty.Register("CurrentFrame", typeof(Frame), typeof(ModifyListUserControl), new PropertyMetadata(0));


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

        private async void Btn_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var item = (sender as HyperlinkButton).Content as IName;
            ItemTappedEvent?.Invoke(item);
            MainRequest request = new MainRequest()
            {
                DataType = RequestServer.DataType.SmallPublication,
                TypeRequest = TypeRequest.Read,
            };
            if (item is Genre)
            {
                request.RecievedRequest = new ReadPublciationRequest()
                {
                    ListGenres = new List<string>() { item.Name },
                    PublicationType = PublicationType.Any
                };
            }

            if (item is Actor)
            {
                request.DataType = RequestServer.DataType.Actor;
                request.RecievedRequest = item.Name;
            }
            var answer = await ServerRequest.SendRequest(request);

            List<VMSmallPublication> result = JsonConvert.DeserializeObject<List<VMSmallPublication>>(answer.ToString());
            await FilesAction.CreatePostersPublications(result);
            CurrentFrame.Navigate(typeof(ContentPage), result);
        }
    }
}