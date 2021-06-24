using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetalTheist.Data.Entities
{
    public enum TypeOfRelease
    {
        LP,
        EP,
        None
    }
    public class Album
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public TypeOfRelease TypeOfRelease { get; set; }
        public string ExtraInformation { get; set; }
        public Band Band { get; set; }
    }
}
