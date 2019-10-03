using Microsoft.EntityFrameworkCore.Migrations;

namespace BarManager.Migrations
{
    public partial class RatingUnrequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Rating",
                table: "Recipe",
                type: "decimal(18, 1)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 1)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Rating",
                table: "Recipe",
                type: "decimal(18, 1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 1)",
                oldNullable: true);
        }
    }
}
