using MetalTheist.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MetalTheist.Data.Entities
{
    public class Article
    {
        public int Id { get; set; }
        [Range(0,100000)]
        public string Content { get; set; }
        [Range(0,100)]
        public string ShortContent { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [Range(1,10)]
        public string Moniker { get; set; } //special identifier
        public DateTime? UploadDate { get; set; }
        //[Required]
        public Author Author { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<Tag> Tags { get; set; } = new List<Tag>();
        public List<Article> RelatedArticles { get; set; } = new List<Article>();
        public ArticleStatistic Statistics { get; set; }
    }
}
