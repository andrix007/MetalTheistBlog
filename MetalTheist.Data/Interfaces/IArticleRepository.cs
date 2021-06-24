using MetalTheist.Core;
using MetalTheist.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetalTheist.Data.Repositories
{
    public interface IArticleRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> CommitAsync();

        Task<List<Article>> GetAllArticlesAsync();
        Task<Article> GetArticleAsyncById(int id);
        Task<Article> GetArticleAsyncByMoniker(string moniker);
        Task<ArticleStatistic> GetArticleStatisticAsync(int id);
    }
}
