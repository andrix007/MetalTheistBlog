using MetalTheist.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetalTheist.Data.Interfaces
{
    public interface IBandRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> CommitAsync();

        Task<List<Band>> GetAllBandsAsync(bool includeAlbums=false, bool includeBandMembers=false);
        Task<Band> GetBandByIdAsync(int id, bool includeAlbums=false, bool includeBandMembers=false);
        Task<List<BandMember>> GetBandMembersAsync(int id, bool includeBandMemberRoles=false);
        Task<List<Album>> GetAlbumsAsync(int id);
    }
}
