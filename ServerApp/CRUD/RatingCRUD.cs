using Model.PublicationTypes;
using ModelDataBase.DBPublicationTypes;
using ServerApp.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp.CRUD
{
    class RatingCRUD 
    {
        private static NewsForumContext CurrentNewsForumContext = NewsForumContext.GetNewsForumContext;


        public static List<Rating> GetSelfRatings(int userId)
        {
            var dbUser = UserCRUD.GetDBUserFromId(userId);

            if (dbUser == null)
                return null;

            return dbUser.ListRatings.Select(r => new Rating()
            {
                Mark = r.Mark,
                PublicationId = r.PublicationId,
                UserId = r.UserId
            }).ToList();
        }


        public static List<Rating> GetPublicationRating(int publicationId)
        {
            var dbPublication = PublicationCRUD.GetDBPublicationFromId(publicationId);

            if (dbPublication == null)
                return null;

            return dbPublication.ListMarks.Select(r => new Rating()
            {
                Mark = r.Mark,
                PublicationId = r.Mark,
                UserId = r.UserId
            }).ToList();

        }

        public static double GetTotalRating(int publicationId)
        {
            var dbPublication = PublicationCRUD.GetDBPublicationFromId(publicationId);

            if (dbPublication != null && dbPublication.ListMarks.Any())
                return Math.Round(dbPublication.ListMarks.Average(r => r.Mark), 1);

            return 0;
        }

        public static bool CreateRating(Rating rating)
        {
            var dbUser = UserCRUD.GetDBUserFromId(rating.UserId);
            var dbPublic = PublicationCRUD.GetDBPublicationFromId(rating.PublicationId);

            if (dbUser != null && dbPublic != null)
            {
                CurrentNewsForumContext.Ratings.Add(new DBRating()
                {
                    Mark = rating.Mark,
                    PublicationId = rating.PublicationId,
                    UserId = rating.UserId
                });
                CurrentNewsForumContext.SaveChanges();
                return true;
            }

            return false;
        }

        public static bool UpdateRating(Rating rating)
        {
            var dbRating = CurrentNewsForumContext.Ratings
                .FirstOrDefault(r => r.PublicationId == rating.PublicationId && r.UserId == rating.UserId);

            dbRating.Mark = rating.Mark;
            CurrentNewsForumContext.SaveChanges();

            return true;
        }
    }
}