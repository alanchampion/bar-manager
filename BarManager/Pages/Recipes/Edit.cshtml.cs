using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BarManager.Models;

namespace BarManager.Pages.Recipes
{
    public class EditModel : PageModel
    {
        private readonly BarManager.Models.BarManagerContext _context;

        public EditModel(BarManager.Models.BarManagerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Recipe Recipe { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // TODO use logged in user
            Recipe = await _context.Recipe.FirstOrDefaultAsync(r => r.RecipeID == id && r.User == "achampion");

            if (Recipe == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // TODO use logged in user
            // TODO what happens if not found? 
            var recipeToUpdate = await _context.Recipe.FirstOrDefaultAsync(r => r.RecipeID == id && r.User == "achampion");
            recipeToUpdate.UpdatedDate = DateTime.Now;

            // Console.WriteLine(recipeToUpdate);

            if (await TryUpdateModelAsync<Recipe>(
                recipeToUpdate,
                "recipe",
                r => r.Name, r => r.Instructions, r => r.Description, r => r.Rating, r => r.Price))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
