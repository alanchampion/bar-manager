using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BarManager.Models;

namespace BarManager.Pages.Recipes
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
        public Recipe Recipe { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // TODO use logged in user
            var emptyRecipe = new Recipe
            {
                User = "achampion",
                UpdatedDate = DateTime.Now,
                AddedDate = DateTime.Now
            };

            if (await TryUpdateModelAsync<Recipe>(
                emptyRecipe,
                "recipe",   // Prefix for form value.
                r => r.Name, r => r.Instructions, r => r.Description, r => r.Rating, r => r.Price))
            {
                _context.Recipe.Add(emptyRecipe);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return RedirectToPage("./Index");
        }
    }
}