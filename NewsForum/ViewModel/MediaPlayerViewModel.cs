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
using System.IO;

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
        
        private void CreatePlayer()
        {
            if (MediaPlayer == null)
                MediaPlayer = new MediaElement();
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
            var index = BaseFileCollection.IndexOf(element);
            if (BaseFileCollection.Count <= 1) //проверям на пустоту списка
            {
                //список пуст, следовательно значение Source у Media убираем
                MediaPlayer.Pause();
                CurrentSong = null;
            }
            else if (index == BaseFileCollection.IndexOf(BaseFileCollection.Last()))
            {
                PlaySound((SoundFileContainer)BaseFileCollection.First());
            }
            else if (index < BaseFileCollection.Count && index == BaseFileCollection.IndexOf(CurrentSong))
            {
                PlaySound((SoundFileContainer)BaseFileCollection[index + 1]);
            }
            base.RemoveElement(element);
        }

        public override void AddElement(IFileSettings element)
        {
            base.AddElement(element);
            CreatePlayer();
        }

        public override void AddRange(IEnumerable<IFileSettings> collection)
        {
            base.AddRange(collection);
            CreatePlayer();
        }

        public void PauseSound()
        {
            MediaPlayer.Pause();
        }

        public void ContinuePlaySound()
        {
            if (CurrentSong == null)
                PlaySound((SoundFileContainer)BaseFileCollection.First());
            else
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