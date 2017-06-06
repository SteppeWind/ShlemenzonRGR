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
        
        public List<VMSmallPublication> ListPublications
        {
            get;
            private set;
        }

        public static async Task<List<VMSmallPublication>> GetSelfPublications()
        {
            List<VMSmallPublication> result = new List<VMSmallPublication>();
            MainRequest request = new MainRequest()
            {
                DataType = RequestServer.DataType.SmallPublication,
                TypeRequest = TypeRequest.ReadSelf,
                UserId = currentUser.UserId
            };
            var answer = await ServerRequest.SendRequest(request);
            result = JsonConvert.DeserializeObject<List<VMSmallPublication>>(answer.SelfAnswer.ToString());
            await FilesAction.CreatePostersPublications(result);
            return result;
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
            var res = JsonConvert.DeserializeObject<User>(answer.SelfAnswer.ToString());
            currentUser.UserId = res.UserId;
            currentUser.Convert(res);
            var ac = currentUser.AccessLevel;
            return true;
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
    }
}