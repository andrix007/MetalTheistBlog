using MetalTheist.Core;
using MetalTheist.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetalTheist.Data.Interfaces
{
    public interface IUserRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> CommitAsync();

        Task<List<User>> GetAllUsersAsync(bool includeBands = false, bool includeArticles = false);
        Task<User> GetUserAsyncById(int id, bool includeBands = false, bool includeArticles = false);
    }
}
