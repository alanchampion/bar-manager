using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BarManager.Migrations
{
    public partial class IngredientGenerateId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredient_Ingredient_IngredientId_IngredientName",
                table: "RecipeIngredient");

            migrationBuilder.DropIndex(
                name: "IX_RecipeIngredient_IngredientId_IngredientName",
                table: "RecipeIngredient");

            migrationBuilder.DropIndex(
                name: "IX_Ingredient_IngredientId",
                table: "Ingredient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredient",
                table: "Ingredient");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Ingredient_IngredientID",
                table: "Ingredient");

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
                oldClrType: typeof(int))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            /*migrationBuilder.AlterColumn<int>(
                name: "IngredientID",
                table: "Ingredient",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);*/

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
                column: "IngredientID");

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
            migrationBuilder.AlterColumn<int>(
                name: "IngredientID",
                table: "Ingredient",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Ingredient_IngredientID",
                table: "Ingredient",
                column: "IngredientID");
        }
    }
}
