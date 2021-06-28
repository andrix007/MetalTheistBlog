using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MetalTheist.Data.Entities
{
    public class BandMemberRole
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}


//Vocalist,
//        Songwriter,
//        Guitarist,
//        Bassist,
//        Drummer,
//        Pianist,
//        Production,
//        SoundEngineer,
//        None