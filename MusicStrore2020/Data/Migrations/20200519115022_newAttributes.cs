using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicStrore2020.Data.Migrations
{
    public partial class newAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Songs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFeatured",
                table: "Songs",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "IsFeatured",
                table: "Songs");
        }
    }
}
