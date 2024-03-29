﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BarManager.Models
{
    public static class DbInitializer
    {
        public static void Initialize(BarManagerContext context)
        {
            try
            {
                var created = context.Database.EnsureCreated();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            

            // Look for any students.
            if (context.Recipe.Any() || context.Ingredient.Any())
            {
                return;   // DB has been seeded
            }

            var recipe = new Recipe
            {
                User = "achampion",
                Name = "Old Fashioned",
                Favorite = true,
                Instructions = "Muddle sugar cube with bitters. Add ice. Pour whiskey over top and stir. Garnish with orange peel and maraschino cherry.",
                Description = "A classic whiskey cocktail",
                Rating = 4.5,
                Price = 6.00M,
                AddedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            context.Recipe.Add(recipe);

            context.SaveChanges();

            var ingredients = new Ingredient[]
            {
                new Ingredient{User = "achampion", Name = "Rye Whiskey", Favorite = true, Owned = true, PurchaseDate = DateTime.Now},
                new Ingredient{User = "achampion", Name = "Angostora Bitters", Favorite = true, Owned = true, PurchaseDate = DateTime.Now},
                new Ingredient{User = "achampion", Name = "Sugar Cube", Favorite = false, Owned = true, PurchaseDate = DateTime.Now},
                new Ingredient{User = "achampion", Name = "Orange Peel", Favorite = false, Owned = true, PurchaseDate = DateTime.Now},
                new Ingredient{User = "achampion", Name = "Maraschino Cherry", Favorite = true, Owned = true, PurchaseDate = DateTime.Now}
            };

            var ingredientIds = new int[ingredients.Length];

            foreach (Ingredient i in ingredients)
            {
                context.Ingredient.Add(i);
            }
            context.SaveChanges();

            var recipeIngredients = new RecipeIngredient[]
            {
                new RecipeIngredient{User = "achampion", RecipeId = recipe.RecipeID, IngredientId = ingredients[0].IngredientID, Amount = "2oz", Required = true},
                new RecipeIngredient{User = "achampion", RecipeId = recipe.RecipeID, IngredientId = ingredients[1].IngredientID, Amount = "4 dashes", Required = true},
                new RecipeIngredient{User = "achampion", RecipeId = recipe.RecipeID, IngredientId = ingredients[2].IngredientID, Amount = "1", Required = true},
                new RecipeIngredient{User = "achampion", RecipeId = recipe.RecipeID, IngredientId = ingredients[3].IngredientID, Amount = "1", Required = false},
                new RecipeIngredient{User = "achampion", RecipeId = recipe.RecipeID, IngredientId = ingredients[4].IngredientID, Amount = "1", Required = false},
            };
            foreach (RecipeIngredient ri in recipeIngredients)
            {
                context.RecipeIngredient.Add(ri);
            }
            context.SaveChanges();
        }
    }
}