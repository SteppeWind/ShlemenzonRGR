using Model;
using Model.PublicationTypes;
using Model.UserTypes;
using NewsForum.Model;
using Newtonsoft.Json;
using RequestServer.AnswerForRequest;
using RequestServer.Request;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ViewModelDataBase;
using ViewModelDataBase.VMPublicationTypes;

namespace NewsForum
{
    class CurrentUser
    {
        private static VMUser currentUser = new VMUser();
        public static VMUser User => currentUser;

        public static event Action ExitEvent = () => { };
       
        public static async Task<List<VMSmallPublication>> GetSelfPublications()
        {
            MainRequest request = new MainRequest()
            {
                DataType = RequestServer.DataType.SmallPublication,
                TypeRequest = TypeRequest.ReadSelf,
                UserId = currentUser.UserId
            };
            var answer = await ServerRequest.SendRequest(request);
            if (answer.SelfAnswer != null)
            {
                currentUser.ListPublications = JsonConvert.DeserializeObject<List<VMSmallPublication>>(answer.SelfAnswer.ToString());
                await FilesAction.CreatePostersPublications(currentUser.ListPublications);
            }
            return currentUser.ListPublications;
        }

        public List<Rating> ListRatings { get; set; }

        public List<Comment> ListComments { get; set; }

        public static async Task<bool> Autorize(string login, string password)
        {
            var answer = await ServerRequest.SendRequest(new MainRequest()
            {
                DataType = RequestServer.DataType.User,
                TypeRequest = TypeRequest.ReadSelf,
                RecievedRequest = login + '%' + password                
            });
            if (answer.SelfAnswer != null)
            {
                var res = JsonConvert.DeserializeObject<User>(answer.SelfAnswer.ToString());
                currentUser.Convert(res);
                var ac = currentUser.AccessLevel;
                return true;
            }

            return false;
        }
        
        public static async Task<List<Comment>> GetSelfComments()
        {
            var result = new List<Comment>();
            var answer = await ServerRequest.SendRequest(new MainRequest()
            {
                DataType = RequestServer.DataType.Comment,
                TypeRequest = TypeRequest.ReadSelf,
                UserId = currentUser.UserId,
            });

            result = JsonConvert.DeserializeObject<List<Comment>>(answer.SelfAnswer.ToString());
            currentUser.ListComments = result;

            return result;
        }

        public static async Task<bool> Create(VMPublication publication)
        {
            bool result = false;
            MainRequest mr = new MainRequest()
            {
                DataType = RequestServer.DataType.Publication,
                TypeRequest = RequestServer.Request.TypeRequest.Create,
                RecievedRequest = publication,
                UserId = User.UserId
            };
            var answer = await ServerRequest.SendRequest(mr);

            if (answer != null && answer.SelfAnswer != null)
            {
                result = (bool)answer.SelfAnswer;
            }

            return result;
        }

        public static async Task<bool> Update(VMUser user)
        {
            var answer = await ServerRequest.SendRequest(new MainRequest()
            {
                DataType = RequestServer.DataType.User,
                TypeRequest = TypeRequest.Update,
                RecievedRequest = user,
                UserId = currentUser.UserId
            });

            return (bool)answer.SelfAnswer;
        }

        public static async Task<bool> Registration(User user)
        {
            var answer = await ServerRequest.SendRequest(new MainRequest()
            {
                DataType = RequestServer.DataType.User,
                TypeRequest = TypeRequest.Create,
                RecievedRequest = user
            });

            return (bool)answer.SelfAnswer;
        }

        public static void Exit()
        {
            currentUser.Convert(new VMUser());
            ExitEvent();
        }
    }
}