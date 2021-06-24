using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MetalTheist.Data.Entities
{
    public class BandMember
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public Band Band { get; set; }
        public DateTime JoinDate { get; set; }
        public DateTime QuitDate { get; set; }
        public List<BandMemberRole> BandMemberRole { get; set; } = new List<BandMemberRole>();
    }
}
