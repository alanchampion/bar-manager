using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BarManager.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using BarManager.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace BarManager.Pages.Ingredients
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly BarManager.Models.BarManagerContext _context;
        private readonly ILogger<IndexModel> _logger;
        private readonly Util _util;
        private readonly DbUtil _dbUtil;

        public IndexModel(IHttpContextAccessor httpContextAccessor, BarManager.Models.BarManagerContext context, ILogger<IndexModel> logger)
        {
            _userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context = context;
            _logger = logger;
            _util = new Util(logger);
            _dbUtil = new DbUtil(context, logger);
        }

        public IList<Ingredient> Ingredients { get;set; }
        public Ingredient item { get; set; }
        private string _userId { get; }

        public async Task OnGetAsync()
        {
            Ingredients = (from r in _context.Ingredient
                           where r.User == _userId
                           orderby r.Name
                           select r).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_dbUtil.IngredientExists(_userId, item.IngredientID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
