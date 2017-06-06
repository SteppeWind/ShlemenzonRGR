using Model.PublicationTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UserTypes
{
    public interface IUser<TPublication, TRating, TComment> 
        where TPublication : ISmallPublication
        where TRating : IRating
        where TComment : IComment
    {
        List<TPublication> ListPublications { get; set; }

        List<TRating> ListRatings { get; set; }

        List<TComment> ListComments { get; set; }
    }
}