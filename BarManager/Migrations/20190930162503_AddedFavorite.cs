using Microsoft.EntityFrameworkCore.Migrations;

namespace BarManager.Migrations
{
    public partial class AddedFavorite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Rating",
                table: "Recipe",
                type: "decimal(18, 1)",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<bool>(
                name: "Favorite",
                table: "Recipe",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Favorite",
                table: "Ingredient",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Favorite",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "Favorite",
                table: "Ingredient");

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "Recipe",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 1)");
        }
    }
}
