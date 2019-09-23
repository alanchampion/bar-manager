using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarManager
{
    public class Util
    {
        private readonly BarManager.Models.BarManagerContext _context;
        private readonly ILogger _logger;

        public Util(BarManager.Models.BarManagerContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public bool IngredientExists(int id)
        {
            return _context.Ingredient.Any(e => e.IngredientID == id);
        }
    }
}
