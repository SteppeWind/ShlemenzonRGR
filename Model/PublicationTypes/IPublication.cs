using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.PublicationTypes
{
    public interface IPublication<TFile, TGenre, TRating, TComment>
        where TFile : IInfoFile
        where TGenre : Genre
        where TRating : IRating
        where TComment : IComment
    {
        List<TFile> ListFiles { get; set; }

        List<TGenre> ListGenres { get; set; }

        List<TRating> ListMarks { get; set; }

        List<TComment> ListComments { get; set; }
    }
}