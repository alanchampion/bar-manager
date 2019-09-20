using Microsoft.EntityFrameworkCore.Migrations;

namespace BarManager.Migrations
{
    public partial class StringLimits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Instructions",
                table: "Recipe",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Recipe",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Instructions",
                table: "Recipe",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Recipe",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
