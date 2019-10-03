using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarManager.Models
{
    public class Ingredient
    {
        public int IngredientID { get; set; }

        // TODO use logged in user
        [Required]
        public string User { get; set; } = "ERROR USER";

        [StringLength(60, MinimumLength = 1, ErrorMessage = "Name can not be longer than 60 characters.")]
        [Required]
        public string Name { get; set; }

        [Required]
        public bool Favorite { get; set; }

        [Required]
        public bool Owned { get; set; }

        [Display(Name = "Purchase Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PurchaseDate { get; set; } = DateTime.MinValue;

        [StringLength(1000, ErrorMessage = "Notes can not be longer than 1000 characters.")]
        public string Notes { get; set; }

        public ICollection<RecipeIngredient> RecipeIngredients { get; set; }

        public override string ToString()
        {
            return "User: " + User + "\n" +
                "Name: " + Name + "\n" +
                "Favorite: " + Favorite + "\n" + 
                "Owned: " + Owned + "\n" +
                "Purchase Date: " + PurchaseDate;
        }
    }
}
