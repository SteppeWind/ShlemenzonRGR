using Model.PublicationTypes;
using NewsForum.Model;
using RequestServer;
using RequestServer.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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

namespace NewsForum.View.MyUserControls.RatingControl
{
    public sealed partial class RatingUserControl : UserControl
    {


        public VMPublication Publication
        {
            get { return (VMPublication)GetValue(PublicationProperty); }
            set
            {
                SetValue(PublicationProperty, value);
                Collection = new SymbolRatingCollection(CurrentRating);
                Collection.ChangeLastTappedRatingEvent += async () =>
                {
                    var request = new MainRequest()
                    {
                        DataType = DataType.Rating,
                        TypeRequest = TypeRequest.Update,
                        RecievedRequest = new Rating()
                        {
                            Mark = Collection.LastTappedRating.Index + 1,
                            PublicationId = Publication.PublicationId,
                            UserId = CurrentUser.User.UserId
                        }
                    };

                    if (CurrentUser.User.ListRatings.FirstOrDefault(r => r.PublicationId == Publication.PublicationId) == null)
                    {
                        request.TypeRequest = TypeRequest.Create;
                    }

                    var answer = await ServerRequest.SendRequest(request);
                    await CurrentUser.GetSelfRatings();

                    if ((bool)answer.SelfAnswer)
                    {
                        MarkTB.Text = $"Ваша оценка: {Collection.LastTappedRating.Index + 1}";
                        StartLoadTotalRating();
                    }
                };
                StartLoadTotalRating();
            }
        }

        // Using a DependencyProperty as the backing store for Publication.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PublicationProperty =
            DependencyProperty.Register("Publication", typeof(VMPublication), typeof(RatingUserControl), null);


        double TotalRating
        {
            get;
            set;
        }

        int CurrentRating
        {
            get
            {
                var rating = CurrentUser.User.ListRatings.FirstOrDefault(r => r.PublicationId == Publication.PublicationId);
                return rating == null ? 0 : rating.Mark;
            }
        }

        SymbolRatingCollection Collection { get; set; }

        public RatingUserControl()
        {
           this.InitializeComponent();
        }

        public async void StartLoadTotalRating()
        {
            TotalRating = await GetTotalRating();
            TotalRatinTB.Text = $"Общая оценка: {TotalRating}";
        }

        private async Task<double> GetTotalRating()
        {
            var answer = await ServerRequest.SendRequest(new MainRequest()
            {
                DataType = DataType.Rating,
                TypeRequest = TypeRequest.Publish,
                RecievedRequest = Publication.PublicationId
            });
            return double.Parse(answer.ToString());
        }

        private void GridView_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Collection.Empty();
        }
    }
}
