using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetalTheist.Data.Entities
{
    public class Statistic
    {
        [Required]
        public int? Likes { get; set; }
        [Required]
        public int? Dislikes { get; set; }
    }
}
