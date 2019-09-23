using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BarManager.Models;

namespace BarManager.Pages.Ingredients
{
    public class CreateModel : PageModel
    {
        private readonly BarManager.Models.BarManagerContext _context;

        public CreateModel(BarManager.Models.BarManagerContext context)
        {
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
                User = "achampion"
            };

            if (await TryUpdateModelAsync<Ingredient>(
                emptyIngredient,
                "ingredient",   // Prefix for form value.
                i => i.Name, i => i.Owned, i => i.PurchaseDate))
            {
                _context.Ingredient.Add(emptyIngredient);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return RedirectToPage("./Index");
        }
    }
}