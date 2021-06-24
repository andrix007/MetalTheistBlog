using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetalTheist.Data.Entities
{
    public class Band
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Website { get; set; }
        public List<Album> Discography { get; set; } = new List<Album>();
        public List<BandMember> BandMembers { get; set; } = new List<BandMember>();
        public List<Genre> Genres { get; set; } = new List<Genre>();
    }
}
