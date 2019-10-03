using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BarManager.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BarManager.Pages.Ingredients
{
    public class CreateModel : PageModel
    {
        private readonly BarManager.Models.BarManagerContext _context;
        private string _userId { get; }
        public CreateModel(IHttpContextAccessor httpContextAccessor, BarManager.Models.BarManagerContext context)
        {
            _userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Ingredient Ingredient { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var emptyIngredient = new Ingredient
            {
                User = _userId
            };

            if (await TryUpdateModelAsync<Ingredient>(
                emptyIngredient,
                "ingredient",   // Prefix for form value.
                i => i.Name, i => i.Favorite, i => i.Owned, i => i.PurchaseDate, i => i.Notes))
            {
                _context.Ingredient.Add(emptyIngredient);
                try
                {
                    await _context.SaveChangesAsync();
                } 
                catch (Exception e)
                {
                    return Page();
                }
                finally
                {
                    _context.ChangeTracker.AutoDetectChangesEnabled = true;
                }

                return RedirectToPage("./Index");
            }

            return RedirectToPage("./Index");
        }
    }
}