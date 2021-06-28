using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MetalTheist.Data.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        [Required]
        [Range(1,100)]
        public string Name { get; set; }
    }
}
