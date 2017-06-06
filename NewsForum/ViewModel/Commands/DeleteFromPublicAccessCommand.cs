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
    class DeleteFromPublicAccessCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            VMSmallPublication publ = parameter as VMSmallPublication;
            TypeRequest typeRequest = TypeRequest.Delete;
            if (publ.IsDeleted)
            {
                typeRequest = TypeRequest.Undelete;
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
                    publ.IsDeleted = !publ.IsDeleted;
                }
            }
        }
    }
}