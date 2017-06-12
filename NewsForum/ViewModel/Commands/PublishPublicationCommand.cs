using NewsForum.Model;
using RequestServer;
using RequestServer.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModelDataBase.VMPublicationTypes;

namespace NewsForum.ViewModel.Commands
{
    class PublishPublicationCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            VMSmallPublication publ = parameter as VMSmallPublication;
            TypeRequest typeRequest = TypeRequest.Publish;
            if (publ.IsPublished)
            {
                typeRequest = TypeRequest.Unpublish;
            }
            var answer = await ServerRequest.SendRequest(new RequestServer.Request.MainRequest()
            {
                DataType = RequestServer.DataType.Publication,
                TypeRequest = typeRequest,
                UserId = publ.UserId,
                RecievedRequest = publ.PublicationId
            });

            if (answer.TypeAnswer == DataType.Bool)
            {
                if ((bool)answer.SelfAnswer)
                {
                    publ.IsPublished = !publ.IsPublished;
                }
            }
        }
    }
}