using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnBoarding.Models;

namespace OnBoarding.Data
{
    public class OnBoardingContext : DbContext
    {
        public OnBoardingContext (DbContextOptions<OnBoardingContext> options)
            : base(options)
        {
        }

        public DbSet<OnBoarding.Models.Quest> Quest { get; set; } = default!;

        public DbSet<OnBoarding.Models.Subdivision> Subdivision { get; set; }
    }
}
