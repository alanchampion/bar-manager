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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ingredient>()
                .HasIndex(i => new { i.Name, i.User })
                .IsUnique();
            modelBuilder.Entity<Recipe>()
                .HasIndex(r => new { r.Name, r.User })
                .IsUnique();
        }
    }
}
