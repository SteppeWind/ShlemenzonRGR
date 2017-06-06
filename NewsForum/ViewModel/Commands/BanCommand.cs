using Model.UserTypes;
using NewsForum.Model;
using RequestServer;
using RequestServer.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NewsForum.ViewModel.Commands
{
    class BanCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            var user = parameter as User;
            TypeRequest typeRequest = TypeRequest.Delete;
            if (user.IsBunned)
            {
                typeRequest = TypeRequest.Undelete;
            }

           var answer = await ServerRequest.SendRequest(new RequestServer.Request.MainRequest()
            {
                DataType = DataType.User,
                TypeRequest = typeRequest,
                UserId = CurrentUser.User.UserId,
                RecievedRequest = user.UserId
            });

            if (answer.TypeAnswer == DataType.Bool)
            {
                if ((bool)answer.SelfAnswer)
                {
                    user.IsBunned = !user.IsBunned;
                }
            }
        }
    }
}