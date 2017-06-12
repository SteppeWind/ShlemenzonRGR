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
using ViewModelDataBase;

namespace NewsForum.ViewModel.Commands
{
    class ChangeAccessLevelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            var user = parameter as VMUser;
            TypeRequest typeRequest = TypeRequest.Publish;
            if (user.AccessLevel == UserAccessLevel.Admin)
            {
                typeRequest = TypeRequest.Unpublish;
            }

            var answer = await ServerRequest.SendRequest(new RequestServer.Request.MainRequest()
            {
                DataType = DataType.User,
                TypeRequest = typeRequest,
                UserId = CurrentUser.User.UserId,
                RecievedRequest = user.UserId
            });

            if (answer.SelfAnswer != null)
            {
                user.AccessLevel = (UserAccessLevel)int.Parse(answer.ToString());
            }
        }
    }
}