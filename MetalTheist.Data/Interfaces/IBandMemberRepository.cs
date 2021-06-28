using MetalTheist.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetalTheist.Data.Interfaces
{
    public interface IBandMemberRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> CommitAsync();

        Task<List<BandMember>> GetAllBandMembersAsync(bool includeBandMemberRoles = false);
        Task<List<BandMember>> GetAllBandMembersForBandAsync(Band band, bool includeBandMemberRoles = false);
        Task<BandMember> GetBandMemberById(int id, bool includeBandMemberRoles = false);
    }
}
