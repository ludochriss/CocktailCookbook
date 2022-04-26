using Microsoft.EntityFrameworkCore.Migrations;

namespace CocktailCookbook.Migrations
{
    public partial class updateGlassWareAGAINagain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Glassware",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Glassware");
        }
    }
}
