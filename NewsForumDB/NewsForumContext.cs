using ModelDataBase.DBPublicationTypes;
using ModelDataBase.DBPublicationTypes.DBNewsTypes;
using ModelDataBase.DBUserTypes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsForumDB
{
    public class NewsForumContext : DbContext
    {
        public NewsForumContext() : base("NewsForumDB") { }

        public DbSet<DBUser> Users { get; set; }
        public DbSet<DBPublication> Publications { get; set; }
        public DbSet<DBActor> Actors { get; set; }
        public DbSet<DBMusicPublication> MusicPublications { get; set; }
        public DbSet<DBGamePubliaction> GamePublications  { get; set; }
        public DbSet<DBFilmPublication> FilmPublications { get; set; }
        public DbSet<DBNewsPublication> NewsPublications { get; set; }
        public DbSet<DBInfoFile> InfoFiles { get; set; }
        public DbSet<DBNewsElement> NewsElements { get; set; }
        public DbSet<DBNewsElementFile> NewsElementFiles { get; set; }
        public DbSet<DBComment> Comments { get; set; }
        public DbSet<DBRating> Ratings { get; set; }
    }
}