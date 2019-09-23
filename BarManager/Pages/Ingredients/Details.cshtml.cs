using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BarManager.Models;

namespace BarManager.Pages.Ingredients
{
    public class DetailsModel : PageModel
    {
        private readonly BarManager.Models.BarManagerContext _context;

        public DetailsModel(BarManager.Models.BarManagerContext context)
        {
            _context = context;
        }

        public Ingredient Ingredient { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Ingredient = await _context.Ingredient.FirstOrDefaultAsync(m => m.IngredientID == id);

            if (Ingredient == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
