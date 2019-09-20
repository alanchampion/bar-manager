using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarManager.Models
{
    public class Recipe
    {
        public int RecipeID { get; set; }

        // TODO pull this from logged in user
        [Required]
        public string User { get; set; } = "ERROR USER";

        [StringLength(60, MinimumLength = 1)]
        [Required]
        public string Name { get; set; }

        [Required]
        public string Instructions { get; set; }

        public string Description { get; set; }

        [Range(1,100)]
        public int Rating { get; set; }

        [Range(1, 100)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Display(Name = "Added Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime AddedDate { get; set; } = DateTime.MinValue;

        [Display(Name = "Updated Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime UpdatedDate { get; set; } = DateTime.MinValue;

        [Display(Name = "Ingredients")]
        [DisplayFormat(NullDisplayText = "No Ingredients")]
        public ICollection<RecipeIngredient> RecipeIngredients { get; set; }
    }
}