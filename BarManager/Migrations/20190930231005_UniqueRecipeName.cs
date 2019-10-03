using Microsoft.EntityFrameworkCore.Migrations;

namespace BarManager.Migrations
{
    public partial class UniqueRecipeName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "User",
                table: "Recipe",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_Name_User",
                table: "Recipe",
                columns: new[] { "Name", "User" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Recipe_Name_User",
                table: "Recipe");

            migrationBuilder.AlterColumn<string>(
                name: "User",
                table: "Recipe",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
