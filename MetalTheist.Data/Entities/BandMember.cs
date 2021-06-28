using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MetalTheist.Data.Entities
{
    public class BandMember
    {
        public int Id { get; set; }
        [Required]
        [Range(1,100)]
        public string Name { get; set; }
        public Band Band { get; set; }
        public DateTime? JoinDate { get; set; } = DateTime.Now;
        public DateTime? QuitDate { get; set; } = DateTime.Now;
        public List<BandMemberRole> BandMemberRoles { get; set; } = new List<BandMemberRole>();
    }
}
