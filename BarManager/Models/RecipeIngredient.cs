using System.ComponentModel.DataAnnotations;

namespace BarManager.Models
{
    public class RecipeIngredient
    {
        public int RecipeIngredientID { get; set; }

        // TODO use logged in user
        [Required]
        public string User { get; set; } = "ERROR USER";

        public int RecipeId{ get; set; }

        public int IngredientId { get; set; }

        [Required]
        [StringLength(30)]
        public string Amount { get; set; }

        [Required]
        public bool Required { get; set; }

        public Recipe Recipe { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}
