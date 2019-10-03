using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BarManager.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace BarManager.Pages.Recipes
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly BarManager.Models.BarManagerContext _context;

        public DeleteModel(IHttpContextAccessor httpContextAccessor, BarManager.Models.BarManagerContext context)
        {
            _userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context = context;
        }

        [BindProperty]
        public Recipe Recipe { get; set; }
        public string ErrorMessage { get; set; }
        private string _userId { get; }

        public async Task<IActionResult> OnGetAsync(string name, bool? saveChangesError = false)
        {
            if (name == null)
            {
                return NotFound();
            }

            Recipe = await _context.Recipe
                        .Include(r => r.RecipeIngredients)
                            .ThenInclude(i => i.Ingredient)
                        .AsNoTracking()
                        .FirstOrDefaultAsync(m => m.Name == name && m.User == _userId);

            if (Recipe == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ErrorMessage = "Delete failed. Try again";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipe
                            .AsNoTracking()
                            .FirstOrDefaultAsync(m => m.RecipeID == id && m.User == _userId);

            if (recipe == null)
            {
                return NotFound();
            }

            try
            {
                _context.Recipe.Remove(recipe);
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
