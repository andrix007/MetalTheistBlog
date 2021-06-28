using MetalTheist.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetalTheist.Data.Extensions
{
    public static class BandMemberMapper
    {
        public static void Map(this BandMember bandMember, BandMember otherBandMember)
        {
            if (otherBandMember.Name != null) bandMember.Name = otherBandMember.Name;
            if (otherBandMember.Band != null) bandMember.Band = otherBandMember.Band;
            if (otherBandMember.JoinDate != null) bandMember.JoinDate = otherBandMember.JoinDate;
            if (otherBandMember.QuitDate != null) bandMember.QuitDate = otherBandMember.QuitDate;
            if (otherBandMember.BandMemberRoles != null) bandMember.BandMemberRoles = otherBandMember.BandMemberRoles;
        }
    }
}
