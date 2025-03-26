using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BOM.Model;

namespace BOM.Data
{
    public class BOMContext : DbContext
    {
        public BOMContext (DbContextOptions<BOMContext> options)
            : base(options)
        {
        }

        public DbSet<BOM.Model.Item> Item { get; set; } = default!;
        public DbSet<BOM.Model.DistintaBase> DistintaBase { get; set; } = default!;
        public DbSet<BOM.Model.VersioneDistintaBase> VersioneDistintaBase { get; set; } = default!;
    }
}
