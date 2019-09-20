using System;
using System.Linq;

namespace BarManager.Models
{
    public static class DbInitializer
    {
        public static void Initialize(BarManagerContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Recipe.Any() || context.Ingredient.Any())
            {
                return;   // DB has been seeded
            }

            // TODO change user to logged in user
            var recipes = new Recipe[]
            {
                new Recipe {
                    User = "achampion",
                    Name = "Old Fashioned",
                    Instructions = "Muddle sugar cube with bitters. Add ice. Pour whiskey over top and stir. Garnish with orange peel and maraschino cherry.",
                    Description = "A classic whiskey cocktail",
                    Rating = 90,
                    Price = 6.00M,
                    AddedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                }
            };
            foreach (Recipe r in recipes)
            {
                context.Recipe.Add(r);
            }
            context.SaveChanges();

            // TODO change user to logged in user
            var ingredients = new Ingredient[]
            {
                new Ingredient{User = "achampion", Name = "Rye Whiskey", Owned = true, PurchaseDate = DateTime.Now},
                new Ingredient{User = "achampion", Name = "Angostora Bitters", Owned = true, PurchaseDate = DateTime.Now},
                new Ingredient{User = "achampion", Name = "Sugar Cube", Owned = true, PurchaseDate = DateTime.Now},
                new Ingredient{User = "achampion", Name = "Orange Peel", Owned = true, PurchaseDate = DateTime.Now},
                new Ingredient{User = "achampion", Name = "Maraschino Cherry", Owned = true, PurchaseDate = DateTime.Now}
            };
            foreach (Ingredient i in ingredients)
            {
                context.Ingredient.Add(i);
            }
            context.SaveChanges();

            var recipeIngredients = new RecipeIngredient[]
            {
                new RecipeIngredient{User = "achampion", RecipeId = 1, IngredientId = 1, Amount = "2oz", Required = true},
                new RecipeIngredient{User = "achampion", RecipeId = 1, IngredientId = 2, Amount = "4 dashes", Required = true},
                new RecipeIngredient{User = "achampion", RecipeId = 1, IngredientId = 3, Amount = "1", Required = true},
                new RecipeIngredient{User = "achampion", RecipeId = 1, IngredientId = 4, Amount = "1", Required = false},
                new RecipeIngredient{User = "achampion", RecipeId = 1, IngredientId = 5, Amount = "1", Required = false},
            };
            foreach (RecipeIngredient ri in recipeIngredients)
            {
                context.RecipeIngredient.Add(ri);
            }
            context.SaveChanges();
        }
    }
}