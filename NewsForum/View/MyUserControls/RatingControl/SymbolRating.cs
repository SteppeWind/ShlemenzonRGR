using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using ViewModelDataBase.VMInterfaces;
using System.Runtime.CompilerServices;

namespace NewsForum.View.MyUserControls.RatingControl
{
    class SymbolRating : IModifyPropertyChanged
    {
        public SymbolIcon Symbol { get; private set; }

        private Symbol icon = Windows.UI.Xaml.Controls.Symbol.OutlineStar;
        public Symbol Icon
        {
            get => icon;
            set
            {
                icon = value;
                ChangeProp();
            }
        }

        private int index = -1;
        public int Index
        {
            get => index;
            set
            {
                index = value;
                ChangeProp();
            }
        }

        public event Action<SymbolRating> MovedEvent = (r) => { };
        public event Action<SymbolRating> TappedEvent = (r) => { };

        public event PropertyChangedEventHandler PropertyChanged;

        public SymbolRating(int index)
        {
            Index = index;
            Symbol = new SymbolIcon(Icon);
            Symbol.PointerMoved += Symbol_PointerMoved;
            Symbol.Tapped += Symbol_Tapped;
        }

        public SymbolRating()
        {

        }

        public void Symbol_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            TappedEvent(this);
        }

        public void Symbol_PointerMoved(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            MovedEvent(this);
        }

        public void ChangeProp([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}