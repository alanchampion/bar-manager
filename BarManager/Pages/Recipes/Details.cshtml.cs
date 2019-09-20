using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BarManager.Models;

namespace BarManager.Pages.Recipes
{
    public class DetailsModel : PageModel
    {
        private readonly BarManager.Models.BarManagerContext _context;

        public DetailsModel(BarManager.Models.BarManagerContext context)
        {
            _context = context;
        }

        public Recipe Recipe { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Recipe = await _context.Recipe
                        .Include(r => r.RecipeIngredients)
                            .ThenInclude(i => i.Ingredient)
                        .AsNoTracking()
                        .FirstOrDefaultAsync(m => m.RecipeID == id);

            if (Recipe == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
