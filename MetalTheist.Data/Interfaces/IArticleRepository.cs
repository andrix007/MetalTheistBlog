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

        Task<List<Article>> GetAllArticlesAsync(bool includeStatistics);
        Task<Article> GetArticleAsyncById(int id, bool includeStatistics = false);
        Task<Article> GetArticleAsyncByMoniker(string moniker, bool includeStatistics = false);
        Task<ArticleStatistic> GetArticleStatisticAsync(int id);
        Task<ArticleStatistic> GetArticleStatisticAsync(string moniker);
    }
}
