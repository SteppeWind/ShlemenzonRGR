using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.PublicationTypes;
using ViewModelDataBase.VMPublicationTypes;
using ModelDataBase.DBPublicationTypes;
using ServerApp.CRUD;
using ViewModelDataBase.VMTypes;
using ServerApp.DataModel;

namespace ServerApp.Converting
{
    class PublicationConverter<TDBPublication, TVMPublication>
        : SmallPublicationConverter<TDBPublication, TVMPublication>
        where TDBPublication : DBPublication, new()
        where TVMPublication : VMPublication, new()
    {
        public override TVMPublication ConvertPublication(TDBPublication publication)
        {
            var result = base.ConvertPublication(publication);
            result.ListGenres = publication.ListGenres.Select(g => new Genre() { Name = g.Name }).ToList();
            result.ListMarks = publication.ListMarks.Select(r =>
            {
                var rat = new Rating();
                rat.Convert(r);
                return rat;
            }).ToList();
            result.ListComments = publication.ListComments.Select(c =>
            {
                var comm = new Comment();
                comm.Convert(c);
                return comm;
            }).ToList();

            result.PosterImage = new VMFile()
            {
                Bytes = FilesManipulation.ConvertFileToBytes(publication.RefPoster),
                Type = $"{publication.RefPoster.Substring(publication.RefPoster.LastIndexOf('.'))}"
            };

            if (publication.ListFiles.Any())
            {
                foreach (var item in publication.ListFiles)
                {
                    result.ListFiles.Add(new VMFile()
                    {
                        Bytes = FilesManipulation.ConvertFileToBytes(item.FullPath),
                        Type = item.Type
                    });
                }

                result.Description = new VMFile()
                {
                    Bytes = FilesManipulation.ConvertFileToBytes(publication.RefDescription),
                    Type = ".rtf"
                };
            }

            return result;
        }

        public override TDBPublication ConvertPublication(TVMPublication publication)
        {
            var result = base.ConvertPublication(publication);
            var genres = publication.ListGenres.Select(g => $"{g.Name}").ToList();
            result.ListGenres = NewsForumContext.GetNewsForumContext.Genres.Where(g => genres.Contains(g.Name)).ToList();

            return result;
        }
    }
}