using NewsForum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsForum.ViewModel.Commands
{
    class PlaySoundCommand : BaseCommand
    {
        public MediaPlayerViewModel PlayerViewModel { get; private set; }


        public PlaySoundCommand(MediaPlayerViewModel viewModel)
        {
            PlayerViewModel = viewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return parameter is SoundFileContainer;
        }

        public override void Execute(object parameter)
        {
            PlayerViewModel.PlaySound((SoundFileContainer)parameter);
        }
    }
}