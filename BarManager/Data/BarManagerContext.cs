using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BarManager.Models
{
    public class BarManagerContext : DbContext
    {
        public BarManagerContext (DbContextOptions<BarManagerContext> options)
            : base(options)
        {
        }

        public DbSet<BarManager.Models.Recipe> Recipe { get; set; }
    }
}
