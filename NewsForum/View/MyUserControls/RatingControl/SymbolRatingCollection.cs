using Model.PublicationTypes;
using NewsForum.Model;
using RequestServer;
using RequestServer.Request;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ViewModelDataBase.VMInterfaces;

namespace NewsForum.View.MyUserControls.RatingControl
{
    class SymbolRatingCollection
    {
        public int Count { get; private set; }

        public ObservableCollection<SymbolRating> ListRatings { get; private set; }
        
        public SymbolRating LastTappedRating { get; set; }

        public event Action ChangeLastTappedRatingEvent = () => { };

        public SymbolRatingCollection(int lastIndex, int count = 5)
        {
            Count = count;
            ListRatings = new ObservableCollection<SymbolRating>();
            LastTappedRating = new SymbolRating();
            for (int i = 0; i < Count; i++)
            {
                var rating = new SymbolRating(i);
                rating.MovedEvent += Rating_MovedEvent;
                rating.TappedEvent += Rating_TappedEvent;
                ListRatings.Add(rating);
            }
            FillBehindIndex(lastIndex);
        }


        private void Rating_TappedEvent(SymbolRating obj)
        {
            LastTappedRating = obj;
            ChangeLastTappedRatingEvent();
        }

        private void Rating_MovedEvent(SymbolRating obj)
        {
            for (int i = 0; i < Count; i++)
            {
                if (i <= obj.Index)
                    ListRatings[i].Icon = Windows.UI.Xaml.Controls.Symbol.SolidStar;
                else
                    ListRatings[i].Icon = Windows.UI.Xaml.Controls.Symbol.OutlineStar;
            }
        }

        public void FillBehindIndex(int index)
        {
            if (index != 0 && index <= Count)
            {
                for (int i = 0; i < index; i++)
                {
                    ListRatings[i].Icon = Windows.UI.Xaml.Controls.Symbol.SolidStar;
                }
                LastTappedRating = ListRatings[index -1];
            }
        }

        public void Empty()
        {
            foreach (var item in ListRatings)
            {
                if (LastTappedRating != null && item.Index <= LastTappedRating.Index)
                {
                    item.Icon = Windows.UI.Xaml.Controls.Symbol.SolidStar;
                    continue;
                }
                item.Icon = Windows.UI.Xaml.Controls.Symbol.OutlineStar;
            }
        }
    }
}