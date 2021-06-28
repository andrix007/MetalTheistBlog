using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MetalTheist.Data.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        [Range(1,999)]
        public string Content { get; set; }
        //[Required]
        public User User { get; set; }
        [Required]
        public DateTime UploadDate { get; set; }
        public Comment ParentComment { get; set; }
    }
}
