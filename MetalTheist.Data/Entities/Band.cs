using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MetalTheist.Data.Entities
{
    public class Band
    {
        public int Id { get; set; }
        [Required]
        [Range(1,200)]
        public string Name { get; set; }
        [Required]
        [Range(1,50)]
        public string Country { get; set; }
        [Required]
        [Range(1,50)]
        public string City { get; set; }
        public string Website { get; set; }
        public List<Album> Discography { get; set; } = new List<Album>();
        //[Required]
        public List<BandMember> BandMembers { get; set; } = new List<BandMember>();
        public List<Genre> Genres { get; set; } = new List<Genre>();
    }
}
