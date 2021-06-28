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
    public class BandMemberRepository : IBandMemberRepository
    {
        private readonly MetalContext metalContext;
        private readonly ILogger<BandMemberRepository> logger;

        public BandMemberRepository(MetalContext metalContext, ILogger<BandMemberRepository> logger)
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

        public async Task<List<BandMember>> GetAllBandMembersAsync(bool includeBandMemberRoles = false)
        {
            logger.LogInformation("Getting all BandMembers");

            IQueryable<BandMember> query = metalContext.BandMembers;
            if (includeBandMemberRoles)
            {
                query = query.Include(bm => bm.BandMemberRoles);
            }
            query = query.OrderBy(bm => bm.Name);

            return await query.ToListAsync();
        }

        public async Task<List<BandMember>> GetAllBandMembersForBandAsync(Band band, bool includeBandMemberRoles = false)
        {
            logger.LogInformation($"Getting all BandMembers for band {band.Name}");

            IQueryable<BandMember> query = metalContext.BandMembers.Where(bm => bm.Band.Id == band.Id);
            if(includeBandMemberRoles)
            {
                query = query.Include(bm => bm.BandMemberRoles);
            }
            query = query.OrderBy(bm => bm.Name);

            return await query.ToListAsync();
        }

        public async Task<BandMember> GetBandMemberById(int id, bool includeBandMemberRoles = false)
        {
            logger.LogInformation($"Getting BandMember with id {id}");

            IQueryable<BandMember> query = metalContext.BandMembers.Where(bm => bm.Id == id);
            if (includeBandMemberRoles)
            {
                query = query.Include(bm => bm.BandMemberRoles);
            }
            query = query.OrderBy(bm => bm.Name);

            return await query.FirstOrDefaultAsync();
        }
    }
}
