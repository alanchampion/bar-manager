using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BarManager.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace BarManager.Pages.Recipes
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly BarManager.Models.BarManagerContext _context;

        public EditModel(IHttpContextAccessor httpContextAccessor, BarManager.Models.BarManagerContext context)
        {
            _userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context = context;
        }

        [BindProperty]
        public Recipe Recipe { get; set; }

        private string _userId { get; }

        public async Task<IActionResult> OnGetAsync(string name)
        {
            if (name == null)
            {
                return NotFound();
            }

            Recipe = await _context.Recipe.FirstOrDefaultAsync(r => r.Name == name && r.User == _userId);

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

            var recipeToUpdate = await _context.Recipe.FirstOrDefaultAsync(r => r.RecipeID == id && r.User == _userId);
            recipeToUpdate.UpdatedDate = DateTime.Now;

            // Console.WriteLine(recipeToUpdate);

            if (await TryUpdateModelAsync<Recipe>(
                recipeToUpdate,
                "recipe",
                r => r.Name, r => r.Instructions, r => r.Description, r => r.Favorite, r => r.Rating, r => r.Price))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
