using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BarManager.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace BarManager.Pages.Ingredients
{
    public class DeleteModel : PageModel
    {
        private readonly BarManager.Models.BarManagerContext _context;

        public DeleteModel(IHttpContextAccessor httpContextAccessor, BarManager.Models.BarManagerContext context)
        {
            _userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context = context;
        }

        [BindProperty]
        public Ingredient Ingredient { get; set; }
        private string _userId { get; }

        public async Task<IActionResult> OnGetAsync(string name)
        {
            if (name == null)
            {
                return NotFound();
            }

            Ingredient = await _context.Ingredient.FirstOrDefaultAsync(m => m.Name == name && m.User == _userId);

            if (Ingredient == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredient = await _context.Ingredient
                            .AsNoTracking()
                            .FirstOrDefaultAsync(m => m.IngredientID == id && m.User == _userId);

            if (ingredient == null)
            {
                return NotFound();
            }

            try
            {
                _context.Ingredient.Remove(ingredient);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("./Delete",
                                     new { id, saveChangesError = true });
            }
        }
    }
}
