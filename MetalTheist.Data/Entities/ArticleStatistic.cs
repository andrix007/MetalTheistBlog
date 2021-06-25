using MetalTheist.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetalTheist.Core
{
    public class ArticleStatistic : Statistic
    {
        public int Id { get; set; }
    }
}
