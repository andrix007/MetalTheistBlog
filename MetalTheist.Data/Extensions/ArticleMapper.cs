using MetalTheist.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetalTheist.Data.Extensions
{
    public static class ArticleMapper
    {
        public static void Map(this Article article, Article otherArticle)
        {
            if (otherArticle.Content != null) article.Content = otherArticle.Content;
            if (otherArticle.ShortContent != null) article.ShortContent = otherArticle.ShortContent;
            if (otherArticle.Title != null) article.Title = otherArticle.Title;
            if (otherArticle.Moniker != null) article.Moniker = otherArticle.Moniker;
            if (otherArticle.UploadDate != null) article.UploadDate = otherArticle.UploadDate;
            if (otherArticle.Author != null) article.Author = otherArticle.Author;
            if (otherArticle.Comments != null) article.Comments = otherArticle.Comments;
            if (otherArticle.Tags != null) article.Tags = otherArticle.Tags;
            if (otherArticle.RelatedArticles != null) article.RelatedArticles = otherArticle.RelatedArticles;
            if (otherArticle.Statistics != null) article.Statistics = otherArticle.Statistics;
        }
    }
}
