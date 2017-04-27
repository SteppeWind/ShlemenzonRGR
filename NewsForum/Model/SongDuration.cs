using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NewsForum.Model
{
    class SongDuration : INotifyPropertyChanged
    {

        private int countMinutes = 0;
        public int CountMinutes
        {
            get => countMinutes;
            set
            {
                countMinutes = value;
                ChangeProperty();
            }
        }

        private int totalSeconds;
        public int TotalSeconds
        {
            get => totalSeconds; 
            set
            {
                totalSeconds = value;
                ChangeProperty();
            }
        }

        private int currentValue;
        public int CurrentValue
        {
            get => currentValue; 
            set
            {
                currentValue = value;
                ChangeProperty();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void ChangeProperty([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
