using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BarManager.Models;
using Microsoft.Extensions.Logging;

namespace BarManager.Pages.Ingredients
{
    public class IndexModel : PageModel
    {
        private readonly BarManager.Models.BarManagerContext _context;
        private readonly ILogger<IndexModel> _logger;
        private readonly Util _util;

        public IndexModel(BarManager.Models.BarManagerContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
            _util = new Util(context, logger);
        }

        public IList<Ingredient> Ingredients { get;set; }
        public Ingredient item { get; set; }

        public async Task OnGetAsync()
        {
            Ingredients = await _context.Ingredient.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_util.IngredientExists(item.IngredientID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
