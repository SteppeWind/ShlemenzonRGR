using Model.UserTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.PublicationTypes
{
    public class Publication<TFile, TGenre, TRating, TComment> : SmallPublication,
        IPublication<TFile, TGenre, TRating, TComment>
        where TFile : IInfoFile
        where TGenre : IGenre
        where TRating : IRating
        where TComment : IComment
    {

        public virtual List<TFile> ListFiles { get; set; }

        public virtual List<TGenre> ListGenres { get; set; }

        public virtual List<TRating> ListMarks { get; set; }

        public virtual List<TComment> ListComments { get; set; }

        public override SmallPublication PublicationComponent
        {
            get => base.PublicationComponent;
            set
            {
                base.PublicationComponent = value;
            }
        }

        public Publication()
        {
            ListFiles = ListFiles ?? new List<TFile>();
            ListGenres = ListGenres ?? new List<TGenre>();
            ListMarks = ListMarks ?? new List<TRating>();
            ListComments = ListComments ?? new List<TComment>();
        }
    }
}