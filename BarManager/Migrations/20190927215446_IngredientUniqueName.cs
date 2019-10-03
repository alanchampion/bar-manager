using Microsoft.EntityFrameworkCore.Migrations;

namespace BarManager.Migrations
{
    public partial class IngredientUniqueName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredient_Ingredient_IngredientID_IngredientName",
                table: "RecipeIngredient");

            migrationBuilder.DropIndex(
                name: "IX_RecipeIngredient_IngredientID_IngredientName",
                table: "RecipeIngredient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredient",
                table: "Ingredient");

            migrationBuilder.DropIndex(
                name: "IX_Ingredient_IngredientID",
                table: "Ingredient");

            /*migrationBuilder.DropColumn(
                name: "IngredientID",
                table: "RecipeIngredient");*/

            migrationBuilder.DropColumn(
                name: "IngredientName",
                table: "RecipeIngredient");

            migrationBuilder.AlterColumn<string>(
                name: "User",
                table: "Ingredient",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredient",
                table: "Ingredient",
                column: "IngredientID");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredient_IngredientId",
                table: "RecipeIngredient",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_Name_User",
                table: "Ingredient",
                columns: new[] { "Name", "User" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredient_Ingredient_IngredientId",
                table: "RecipeIngredient",
                column: "IngredientId",
                principalTable: "Ingredient",
                principalColumn: "IngredientID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredient_Ingredient_IngredientId",
                table: "RecipeIngredient");

            migrationBuilder.DropIndex(
                name: "IX_RecipeIngredient_IngredientId",
                table: "RecipeIngredient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredient",
                table: "Ingredient");

            migrationBuilder.DropIndex(
                name: "IX_Ingredient_Name_User",
                table: "Ingredient");

            migrationBuilder.AddColumn<int>(
                name: "IngredientID",
                table: "RecipeIngredient",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IngredientName",
                table: "RecipeIngredient",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "User",
                table: "Ingredient",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredient",
                table: "Ingredient",
                columns: new[] { "IngredientID", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredient_IngredientID_IngredientName",
                table: "RecipeIngredient",
                columns: new[] { "IngredientID", "IngredientName" });

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_IngredientID",
                table: "Ingredient",
                column: "IngredientID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredient_Ingredient_IngredientID_IngredientName",
                table: "RecipeIngredient",
                columns: new[] { "IngredientID", "IngredientName" },
                principalTable: "Ingredient",
                principalColumns: new[] { "IngredientID", "Name" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
