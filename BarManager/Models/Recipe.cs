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

        [StringLength(60, MinimumLength = 1, ErrorMessage = "Name can not be longer than 60 characters.")]
        [Required]
        public string Name { get; set; }

        [StringLength(1000, ErrorMessage = "Instructions can not be longer than 1000 characters.")]
        [Required]
        public string Instructions { get; set; }

        [StringLength(100, ErrorMessage = "Description can not be longer than 100 characters.")]
        public string Description { get; set; }

        [Range(0,100, ErrorMessage = "Rating must be between 0 and 100.")]
        public int Rating { get; set; }

        [Range(0, 1000, ErrorMessage = "Price must be between $0 and $1000.")]
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

        public override string ToString()
        {
            return "User: " + User + "\n" +
                "Name: " + Name + "\n" +
                "Instructions: " + Instructions + "\n" +
                "Description: " + Description + "\n" +
                "Rating: " + Rating + "\n" +
                "Price: " + Price + "\n" +
                "AddedDate: " + AddedDate + "\n" + 
                "UpdatedDate: " + UpdatedDate;
        }
    }
}