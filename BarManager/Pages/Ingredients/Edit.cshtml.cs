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
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BarManager.Pages.Ingredients
{
    public class EditModel : PageModel
    {
        private readonly BarManager.Models.BarManagerContext _context;
        private readonly ILogger<EditModel> _logger;
        private Util _util;
        private DbUtil _dbUtil;

        public EditModel(IHttpContextAccessor httpContextAccessor, BarManager.Models.BarManagerContext context, ILogger<EditModel> logger)
        {
            _userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context = context;
            _logger = logger;
            _util = new Util(logger);
            _dbUtil = new DbUtil(context, logger);
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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var ingredientToUpdate = await _context.Ingredient.FirstOrDefaultAsync(i => i.IngredientID == id && i.User == _userId);

            Console.WriteLine(ingredientToUpdate);

            if (await TryUpdateModelAsync<Ingredient>(
                ingredientToUpdate,
                "ingredient",
                i => i.Name, i => i.Favorite, i => i.PurchaseDate, i => i.Owned, i => i.Notes))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
