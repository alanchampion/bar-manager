using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BarManager.Models
{
    public class Ingredient
    {
        public int IngredientID { get; set; }

        [Required]
        public string User { get; set; }

        [StringLength(60, MinimumLength = 1)]
        [Required]
        public string Name { get; set; }

        public ICollection<RecipeIngredient> Recipes { get; set; }
    }
}
