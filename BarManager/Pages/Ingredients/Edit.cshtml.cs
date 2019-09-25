using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BarManager.Models;
using Microsoft.Extensions.Logging;

namespace BarManager.Pages.Ingredients
{
    public class EditModel : PageModel
    {
        private readonly BarManager.Models.BarManagerContext _context;
        private readonly ILogger<EditModel> _logger;
        private Util _util;
        private DbUtil _dbUtil;

        public EditModel(BarManager.Models.BarManagerContext context, ILogger<EditModel> logger)
        {
            _context = context;
            _logger = logger;
            _util = new Util(logger);
            _dbUtil = new DbUtil(context, logger);
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Ingredient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_dbUtil.IngredientExists("achampion", Ingredient.IngredientID))
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
