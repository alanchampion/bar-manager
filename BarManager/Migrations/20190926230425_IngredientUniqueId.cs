using Microsoft.EntityFrameworkCore.Migrations;

namespace BarManager.Migrations
{
    public partial class IngredientUniqueId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_Ingredient_IngredientID",
                table: "Ingredient",
                column: "IngredientID");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_IngredientID",
                table: "Ingredient",
                column: "IngredientID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Ingredient_IngredientID",
                table: "Ingredient");

            migrationBuilder.DropIndex(
                name: "IX_Ingredient_IngredientID",
                table: "Ingredient");
        }
    }
}
