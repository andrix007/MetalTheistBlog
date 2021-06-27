using MetalTheist.Data.Entities;
using MetalTheist.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetalTheist.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MetalContext metalContext;
        private readonly ILogger<UserRepository> logger;

        public UserRepository(MetalContext metalContext, ILogger<UserRepository> logger)
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

        public async Task<List<User>> GetAllUsersAsync(bool includeBands = false, bool includeArticles = false)
        {
            logger.LogInformation("Getting all Users");

            IQueryable<User> query = metalContext.Users;
            if(includeBands)
            {
                query = query.Include(u => u.Bands);
            }
            if(includeArticles)
            {
                query = query.Include(u => u.Articles);
            }
            query.OrderBy(u => u.Username);

            return await query.ToListAsync();
        }

        public async Task<User> GetUserAsyncById(int id, bool includeBands = false, bool includeArticles = false)
        {
            logger.LogInformation($"Getting User with id {id}");

            IQueryable<User> query = metalContext.Users.Where(u => u.Id == id);
            if (includeBands)
            {
                query = query.Include(u => u.Bands);
            }
            if (includeArticles)
            {
                query = query.Include(u => u.Articles);
            }
            query.OrderBy(u => u.Username);

            return await query.FirstOrDefaultAsync();
        }
    }
}
