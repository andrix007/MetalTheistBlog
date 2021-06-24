using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetalTheist.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime DateOfRegistration { get; set; }
        public List<Band> Bands { get; set; } = new List<Band>();
        public List<Article> Articles { get; set; } = new List<Article>();
    }
}
