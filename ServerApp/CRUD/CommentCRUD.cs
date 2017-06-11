using Model.PublicationTypes;
using Model.UserTypes;
using ModelDataBase.DBPublicationTypes;
using ServerApp.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelDataBase.VMPublicationTypes;

namespace ServerApp.CRUD
{
    class CommentCRUD
    {
        private static NewsForumContext CurrentNewsForumContext = NewsForumContext.GetNewsForumContext;

        public static DBComment GetDBCommentFromId(int id) => CurrentNewsForumContext.Comments.FirstOrDefault(c => c.CommentId == id);

        public static List<VMComment> GetComments(int publicationId)
        {
            var result = new List<VMComment>();

            var dbComments = CurrentNewsForumContext.Comments.Where(c => c.PublicationId == publicationId);

            dbComments.ToList().ForEach(dbc =>
            {
                User us = new User();
                us.Convert(dbc.User);
                result.Add(new VMComment()
                {
                    UserId = us.UserId,
                    Value = dbc.Value,
                    PublicationId = dbc.PublicationId,
                    User = us,
                    CommentId = dbc.CommentId
                });
            });

            return result;
        }


        public static List<Comment> GetUserComments(int userId)
        {
            var result = new List<Comment>();

            var dbComments = CurrentNewsForumContext.Comments.Where(c => c.UserId == userId);

            dbComments.ToList().ForEach(dbc =>
            {
                User us = new User();
                us.Convert(dbc.User);
                result.Add(new Comment()
                {
                    UserId = us.UserId,
                    Value = dbc.Value,
                    PublicationId = dbc.PublicationId,
                    CommentId = dbc.CommentId
                });
            });

            return result;
        }

        public static bool UpdateComment(Comment comment)
        {
            var dbUser = UserCRUD.GetDBUserFromId(comment.UserId);
            var dbComment = GetDBCommentFromId(comment.CommentId);

            if ((dbUser != null && dbUser.AccessLevel >= UserAccessLevel.Admin) || dbUser.ListComments.Contains(dbComment))
            {
                dbComment.Value = comment.Value;
                CurrentNewsForumContext.SaveChanges();
                return true;
            }

            return false;
        }


        public static VMComment AddComment(Comment comment)
        {
            var dbUser = UserCRUD.GetDBUserFromId(comment.UserId);
            var dbPublication = PublicationCRUD.GetDBPublicationFromId(comment.PublicationId);
            VMComment result = new VMComment();


            if (dbUser != null && dbPublication != null)
            {
                DBComment dbComment = new DBComment()
                {
                    PublicationId = dbPublication.PublicationId,
                    UserId = dbUser.UserId,
                    Value = comment.Value
                };
                CurrentNewsForumContext.Comments.Add(dbComment);
                CurrentNewsForumContext.SaveChanges();

                dbComment = CurrentNewsForumContext.Comments.ToList().Last();
                result.Convert(dbComment);
                result.User.Convert(dbComment.User);
                return result;
            }

            return null;
        }
        
        public static bool DeleteComment(int idComment, int idUser)
        {
            var dbUser = UserCRUD.GetDBUserFromId(idUser);
            var dbComment = CurrentNewsForumContext.Comments.FirstOrDefault(c => c.CommentId == idComment);

            if (dbComment != null && dbUser != null)
            {
                if (dbComment.User.AccessLevel == UserAccessLevel.God)
                    return false;



                if (dbUser.AccessLevel >= UserAccessLevel.Admin || dbUser.ListComments.Contains(dbComment))
                {
                    CurrentNewsForumContext.Comments.Remove(dbComment);
                    CurrentNewsForumContext.SaveChanges();
                    return true;
                }
                
            }

            return false;
        }
    }
}