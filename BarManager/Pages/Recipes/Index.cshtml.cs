using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BarManager.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BarManager.Pages.Recipes
{
    public class IndexModel : PageModel
    {
        private readonly BarManager.Models.BarManagerContext _context;

        public IndexModel(BarManager.Models.BarManagerContext context)
        {
            _context = context;
        }

        public IList<Recipe> Recipe { get;set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public SelectList Ingredients { get; set; }
        [BindProperty(SupportsGet = true)]
        public string RecipeIngredients { get; set; }

        public async Task OnGetAsync()
        {
            // Use LINQ to get list of genres.
            /*IQueryable<string> ingridientsQuery = from m in _context.Recipe
                                            orderby m.Ingredients
                                            select m.Ingredients;*/

            var recipes = from m in _context.Recipe
                         select m;

            if (!string.IsNullOrEmpty(SearchString))
            {
                recipes = recipes.Where(s => s.Name.Contains(SearchString));
            }

            /*if (!string.IsNullOrEmpty(RecipeIngredients))
            {
                recipes = recipes.Where(x => x.Ingredients == Ingredients);
            }*/

            // Ingredients = new SelectList(await ingridientsQuery.Distinct().ToListAsync());
            Recipe = await recipes.ToListAsync();
        }
    }
}
