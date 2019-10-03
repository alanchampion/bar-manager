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

namespace BarManager.Pages.Ingredients
{
    public class DetailsModel : PageModel
    {
        private readonly BarManager.Models.BarManagerContext _context;

        public DetailsModel(IHttpContextAccessor httpContextAccessor, BarManager.Models.BarManagerContext context)
        {
            _userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context = context;
        }

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
    }
}
