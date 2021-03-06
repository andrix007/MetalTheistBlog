using MetalTheist.Core;
using MetalTheist.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetalTheist.Data.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly MetalContext metalContext;
        private readonly ILogger<ArticleRepository> logger;

        public ArticleRepository(MetalContext metalContext, ILogger<ArticleRepository> logger)
        {
            this.metalContext = metalContext;
            this.logger = logger;
        }

        public void Add<T>(T entity) where T : class
        {
            logger.LogInformation($"Adding an object of type {entity.GetType()} to the context.");
            metalContext.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            logger.LogInformation($"Removing an object of type {entity.GetType()} to the context.");
            metalContext.Remove(entity);
        }

        public async Task<bool> CommitAsync()
        {
            logger.LogInformation($"Attempitng to save the changes in the context");
            return (await metalContext.SaveChangesAsync()) > 0;
        }

        public async Task<List<Article>> GetAllArticlesAsync(bool includeStatistics)
        {
            logger.LogInformation("Getting all Articles");

            IQueryable<Article> query = metalContext.Articles;
            if(includeStatistics)
            {
                query = query.Include(a => a.Statistics);
            }    
            query = query.OrderBy(a => a.Title);

            return await query.ToListAsync();
        }

        public async Task<Article> GetArticleAsyncById(int id, bool includeStatistics = false)
        {
            logger.LogInformation($"Getting an Article for id: {id}");

            IQueryable<Article> query = metalContext.Articles.Where(a => a.Id == id);
            if(includeStatistics)
            {
                query = query.Include(a => a.Statistics);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Article> GetArticleAsyncByMoniker(string moniker, bool includeStatistics = false)
        {
            logger.LogInformation($"Getting an Article for moniker: {moniker}");

            IQueryable<Article> query = metalContext.Articles.Where(a => a.Moniker == moniker);
            if(includeStatistics)
            {
                query = query.Include(a => a.Statistics);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<ArticleStatistic> GetArticleStatisticAsync(int id)
        {
            var article = await GetArticleAsyncById(id,true);

            logger.LogInformation($"Getting the statistics for article {article.Title} ");

            return article.Statistics;
        }

        public async Task<ArticleStatistic> GetArticleStatisticAsync(string moniker)
        {
            var article = await GetArticleAsyncByMoniker(moniker,true);

            logger.LogInformation($"Getting the statistics for article {article.Title} ");

            return article.Statistics;
        }
    }
}
