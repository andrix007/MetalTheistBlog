using MetalTheist.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetalTheist.Data.Entities
{
    public class Author : User
    {
        public List<Article> WrittenArticles { get; set; } = new List<Article>();
        public AuthorStatistic Statistics { get; set; }
    }
}
