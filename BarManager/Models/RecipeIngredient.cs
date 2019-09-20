using System.ComponentModel.DataAnnotations;

namespace BarManager.Models
{
    public class RecipeIngredient
    {
        public int RecipeIngredientID { get; set; }

        [Required]
        public string User { get; set; }

        public int RecipeId{ get; set; }

        public int IngredientId { get; set; }

        [Required]
        [StringLength(30)]
        public string Amount { get; set; }
    }
}
