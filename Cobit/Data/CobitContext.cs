using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Cobit.Models;

namespace Cobit.Data
{
    public class CobitContext : DbContext
    {
        public CobitContext (DbContextOptions<CobitContext> options)
            : base(options)
        {
        }

        public DbSet<Cobit.Models.AGModel> AGModels { get; set; } = default!;
        public DbSet<Cobit.Models.EDMModel> EDMModel { get; set; } = default!;
        public DbSet<Cobit.Models.Person> Person { get; set; } = default!;
    }
}
