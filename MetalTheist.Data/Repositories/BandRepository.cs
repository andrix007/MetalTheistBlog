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
    public class BandRepository : IBandRepository
    {
        private readonly ILogger<BandRepository> logger;
        private readonly MetalContext metalContext;

        public BandRepository(ILogger<BandRepository> logger, MetalContext metalContext)
        {
            this.logger = logger;
            this.metalContext = metalContext;
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

        public async Task<List<Album>> GetAlbumsAsync(int id)
        {
            logger.LogInformation("Getting all the albums");

            IQueryable<Band> query = metalContext.Bands.Where(b => b.Id == id);
            query = query.Include(b => b.Discography);
            query = query.OrderBy(b => b.Name);

            var albums = await query.FirstOrDefaultAsync();

            return albums.Discography;
        }

        public async Task<List<Band>> GetAllBandsAsync(bool includeAlbums=false, bool includeBandMembers=false)
        {
            logger.LogInformation("Getting all the Bands");

            IQueryable<Band> query = metalContext.Bands;
            if (includeAlbums)
            {
                query = query.Include(b => b.Discography);
            }
            if(includeBandMembers)
            {
                query = query.Include(b => b.BandMembers);
            }
            query = query.OrderBy(a => a.Name);

            return await query.ToListAsync();
        }

        public async Task<Band> GetBandByIdAsync(int id, bool includeAlbums=false, bool includeBandMembers=false)
        {
            logger.LogInformation($"Getting a Band for id: {id}");

            IQueryable<Band> query = metalContext.Bands.Where(b => b.Id == id);
            if (includeAlbums)
            {
                query = query.Include(b => b.Discography);
            }
            if (includeBandMembers)
            {
                query = query.Include(b => b.BandMembers);
            }
            query = query.OrderBy(a => a.Name);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<BandMember>> GetBandMembersAsync(int id, bool includeBandMemberRoles)
        {
            logger.LogInformation($"Getting BandMembers for Band with id {id}");

            var band = await GetBandByIdAsync(id,includeBandMembers:true);

            return band.BandMembers.OrderBy(bm => bm.Name).ToList();
        }

    }
}
