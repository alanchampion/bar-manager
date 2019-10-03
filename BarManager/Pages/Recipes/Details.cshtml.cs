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
using Microsoft.AspNetCore.Authorization;

namespace BarManager.Pages.Recipes
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly BarManager.Models.BarManagerContext _context;

        public DetailsModel(IHttpContextAccessor httpContextAccessor, BarManager.Models.BarManagerContext context)
        {
            _userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context = context;
        }

        public Recipe Recipe { get; set; }
        private string _userId { get; }

        public async Task<IActionResult> OnGetAsync(string name)
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
            return Page();
        }
    }
}
