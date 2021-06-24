using MetalTheist.Core;
using MetalTheist.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetalTheist.Data
{
    public class MetalContext : DbContext
    {
        public MetalContext(DbContextOptions<MetalContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Band> Bands { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<BandMember> BandMembers { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<AuthorStatistic> AuthorStatistics { get; set; }
        public DbSet<ArticleStatistic> ArticleStatistics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>()
                .HasData(new
                {
                    Id = 1,
                    Content = "Hello your computer has virus!",
                    ShortContent = "Hello virus!",
                    Title = "Hello",
                    Moniker = "HEL",
                    UploadDate = DateTime.Now
                });
        }

    }
}
