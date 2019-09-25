using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarManager
{
    public class DbUtil
    {
        private readonly BarManager.Models.BarManagerContext _context;
        private readonly ILogger _logger;

        public DbUtil(BarManager.Models.BarManagerContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public bool IngredientExists(string user, int id)
        {
            // return _context.Ingredient.Any(e => e.IngredientID == id);
            return _context.Ingredient.Any(e => e.IngredientID == id && e.User == user);
        }

        public bool RecipeExists(string user, int id)
        {
            return _context.Recipe.Any(e => e.RecipeID == id && e.User == user);
        }
    }
}
