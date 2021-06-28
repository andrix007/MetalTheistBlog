using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MetalTheist.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [Range(1,20)]
        public string Username { get; set; }
        [Required]
        [Range(8,30)]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime? DateOfRegistration { get; set; } = DateTime.Now;
        public List<Band> Bands { get; set; } = new List<Band>();
        public List<Article> Articles { get; set; } = new List<Article>();
    }
}
