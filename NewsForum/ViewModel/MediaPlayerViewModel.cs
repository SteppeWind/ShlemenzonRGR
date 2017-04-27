using NewsForum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using NewsForum.ViewModel.Commands;
using Windows.UI.Xaml;
using Windows.Storage.Streams;

namespace NewsForum.ViewModel
{
    class MediaPlayerViewModel : BaseCollectionViewModel
    {
        public readonly DependencyProperty MediaPlayerProperty = DependencyProperty.Register("MediaPlayer",
                                                                                             typeof(MediaElement),
                                                                                             typeof(MediaPlayerViewModel),
                                                                                             new PropertyMetadata(new MediaElement()));

        public readonly DependencyProperty CurrentSongProperty = DependencyProperty.Register("CurrentSong",
                                                                                             typeof(SoundFileContainer),
                                                                                             typeof(MediaPlayerViewModel),
                                                                                             new PropertyMetadata(new SoundFileContainer()));
        

        public SoundFileContainer CurrentSong
        {
            get => (SoundFileContainer)GetValue(CurrentSongProperty);
            set => SetValue(CurrentSongProperty, value);
        }
        
        
        public PlaySoundCommand PlaySoundCommand { get; private set; }

        public MediaPlayerViewModel()
        {
            PlaySoundCommand = new PlaySoundCommand(this);
        }    
        
        
        public MediaElement MediaPlayer
        {
            get => (MediaElement)GetValue(MediaPlayerProperty);
            set => SetValue(MediaPlayerProperty, value);
        }

        public void PlaySound(SoundFileContainer sound)
        {
            CurrentSong = sound;
            MediaPlayer.SetSource(sound.AccessStream, "");
        }

        public override void RemoveElement(IFileSettings element)
        {
            var index = BaseFileCollection.IndexOf(element) + 1;
            base.RemoveElement(element);
            if (BaseFileCollection.Count == 0) //проверям на пустоту списка
            {
                //список пуст, следовательно значение Source у Media убираем
            }
            if (index < BaseFileCollection.Count)
            {
                PlaySound((SoundFileContainer)BaseFileCollection[index]);
            }
        }

        public void PauseSound()
        {
            MediaPlayer.Pause();
        }

        public void ContinuePlaySound()
        {
            MediaPlayer.Play();
        }

        public void PlayNextSong()
        {
            if (CurrentSong != null)
            {
                var index = BaseFileCollection.IndexOf(CurrentSong);
                if (index + 1 == BaseFileCollection.Count)
                {
                    CurrentSong = (SoundFileContainer)BaseFileCollection.First();
                }
                else
                {
                    CurrentSong = (SoundFileContainer)BaseFileCollection[index + 1];
                }
            }
            else
            {
                CurrentSong = (SoundFileContainer)BaseFileCollection.First();
            }
            PlaySound(CurrentSong);
        }

        public void PlayPreviousSong()
        {
            if (CurrentSong != null)
            {
                var index = BaseFileCollection.IndexOf(CurrentSong);
                if (index == 0)
                {
                    CurrentSong = (SoundFileContainer)BaseFileCollection.Last();
                }
                else
                {
                    CurrentSong = (SoundFileContainer)BaseFileCollection[index - 1];
                }
            }
            else
            {
                CurrentSong = (SoundFileContainer)BaseFileCollection.Last();
            }
            PlaySound(CurrentSong);
        }
    }
}