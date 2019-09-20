using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BarManager.Models;

namespace BarManager.Pages.Recipes
{
    public class IndexModel : PageModel
    {
        private readonly BarManager.Models.BarManagerContext _context;

        public IndexModel(BarManager.Models.BarManagerContext context)
        {
            _context = context;
        }

        public PaginatedList<Recipe> Recipe { get; set; }
        public string NameSort { get; set; }
        public string DescriptionSort { get; set; }
        public string AddedDateSort { get; set; }
        public string UpdateDateSort { get; set; }
        public string RatingSort { get; set; }
        public string PriceSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;

            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DescriptionSort = sortOrder == "Description" ? "description_desc" : "Description";
            AddedDateSort = sortOrder == "AddedDate" ? "addeddate_desc" : "AddedDate";
            UpdateDateSort = sortOrder == "UpdateDate" ? "updatedate_desc" : "UpdateDate";
            RatingSort = sortOrder == "Rating" ? "rating_desc" : "Rating";
            PriceSort = sortOrder == "Price" ? "price_desc" : "Price";

            if(searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            IQueryable<Recipe> recipeIQ = from r in _context.Recipe
                                            select r;

            if (!String.IsNullOrEmpty(searchString))
            {
                // If errors with searching occure, add .ToUpper. 
                recipeIQ = recipeIQ.Where(r => r.Name.Contains(searchString)
                                       || r.Description.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    recipeIQ = recipeIQ.OrderByDescending(r => r.Name);
                    break;
                case "Description":
                    recipeIQ = recipeIQ.OrderBy(r => r.Description);
                    break;
                case "description_desc":
                    recipeIQ = recipeIQ.OrderByDescending(s => s.Description);
                    break;
                case "AddedDate":
                    recipeIQ = recipeIQ.OrderBy(r => r.AddedDate);
                    break;
                case "addeddate_desc":
                    recipeIQ = recipeIQ.OrderByDescending(s => s.AddedDate);
                    break;
                case "UpdateDate":
                    recipeIQ = recipeIQ.OrderBy(r => r.UpdatedDate);
                    break;
                case "updatedate_desc":
                    recipeIQ = recipeIQ.OrderByDescending(s => s.UpdatedDate);
                    break;
                case "Rating":
                    recipeIQ = recipeIQ.OrderBy(r => r.Rating);
                    break;
                case "rating_desc":
                    recipeIQ = recipeIQ.OrderByDescending(s => s.Rating);
                    break;
                case "Price":
                    recipeIQ = recipeIQ.OrderBy(r => r.Price);
                    break;
                case "price_desc":
                    recipeIQ = recipeIQ.OrderByDescending(s => s.Price);
                    break;
                default:
                    recipeIQ = recipeIQ.OrderBy(s => s.Name);
                    break;
            }

            // TODO let this value be changed
            int pageSize = 10;
            Recipe = await PaginatedList<Recipe>.CreateAsync(
                recipeIQ.AsNoTracking(), pageIndex ?? 1, pageSize);

            // Recipe = await recipeIQ.AsNoTracking().ToListAsync();
        }
    }
}
