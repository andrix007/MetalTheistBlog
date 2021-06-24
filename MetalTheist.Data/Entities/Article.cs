using MetalTheist.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetalTheist.Data.Entities
{
    public class Article
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string ShortContent { get; set; }
        public string Title { get; set; }
        public string Moniker { get; set; } //special identifier
        public DateTime UploadDate { get; set; }
        public Author Author { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<Tag> Tags { get; set; } = new List<Tag>();
        public List<Article> RelatedArticles { get; set; } = new List<Article>();
        public ArticleStatistic Statistics { get; set; }
    }
}
