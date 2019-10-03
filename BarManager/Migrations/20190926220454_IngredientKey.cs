using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BarManager.Migrations
{
    public partial class IngredientKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            /*migrationBuilder.AddColumn<int>(
                name: "IngredientID",
                table: "RecipeIngredient",
                nullable: true);*/

            migrationBuilder.AddColumn<string>(
                name: "IngredientName",
                table: "RecipeIngredient",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecipeName",
                table: "RecipeIngredient",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IngredientID2",
                table: "Ingredient",
                nullable: true);

            migrationBuilder.Sql("update Ingredient set IngredientID2 = IngredientID");

            migrationBuilder.DropColumn(
                name: "IngredientID",
                table: "Ingredient");

            migrationBuilder.RenameColumn(
                name: "IngredientID2",
                table: "Ingredient",
                newName: "IngredientID");

            migrationBuilder.AlterColumn<int>(
                name: "IngredientID",
                table: "Ingredient",
                nullable: false,
                oldNullable: true);

            /*migrationBuilder.AlterColumn<int>(
                name: "IngredientID",
                table: "Ingredient",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);*/

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredient",
                table: "Ingredient",
                columns: new[] { "IngredientID", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredient_IngredientID_IngredientName",
                table: "RecipeIngredient",
                columns: new[] { "IngredientID", "IngredientName" });

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredient_Ingredient_IngredientID_IngredientName",
                table: "RecipeIngredient",
                columns: new[] { "IngredientID", "IngredientName" },
                principalTable: "Ingredient",
                principalColumns: new[] { "IngredientID", "Name" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "IngredientID",
                table: "RecipeIngredient");

            migrationBuilder.DropColumn(
                name: "IngredientName",
                table: "RecipeIngredient");

            migrationBuilder.DropColumn(
                name: "RecipeName",
                table: "RecipeIngredient");

            migrationBuilder.AlterColumn<int>(
                name: "IngredientID",
                table: "Ingredient",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredient",
                table: "Ingredient",
                column: "IngredientID");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredient_IngredientId",
                table: "RecipeIngredient",
                column: "IngredientId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredient_Ingredient_IngredientId",
                table: "RecipeIngredient",
                column: "IngredientId",
                principalTable: "Ingredient",
                principalColumn: "IngredientID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
