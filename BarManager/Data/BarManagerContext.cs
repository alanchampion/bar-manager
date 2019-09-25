using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BarManager.Models
{
    public class BarManagerContext : DbContext
    {
        public BarManagerContext (DbContextOptions<BarManagerContext> options) : base(options){ }

        public DbSet<Recipe> Recipe { get; set; }
        public DbSet<Ingredient> Ingredient { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredient { get; set; }
    }
}
