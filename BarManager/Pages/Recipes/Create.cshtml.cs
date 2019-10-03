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
using Microsoft.AspNetCore.Authorization;

namespace BarManager.Pages.Recipes
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly BarManager.Models.BarManagerContext _context;

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
        public Recipe Recipe { get; set; }
        private string _userId { get; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var emptyRecipe = new Recipe
            {
                User = _userId,
                UpdatedDate = DateTime.Now,
                AddedDate = DateTime.Now
            };

            if (await TryUpdateModelAsync<Recipe>(
                emptyRecipe,
                "recipe",   // Prefix for form value.
                r => r.Name, r => r.Instructions, r => r.Description, r => r.Rating, r => r.Price))
            {
                _context.Recipe.Add(emptyRecipe);
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