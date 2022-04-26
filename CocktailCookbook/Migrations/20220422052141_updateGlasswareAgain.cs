using Microsoft.EntityFrameworkCore.Migrations;

namespace CocktailCookbook.Migrations
{
    public partial class updateGlasswareAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Glassware",
                table: "Cocktail");

            migrationBuilder.AddColumn<int>(
                name: "GlasswareId",
                table: "Cocktail",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Glassware",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Glassware", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cocktail_GlasswareId",
                table: "Cocktail",
                column: "GlasswareId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cocktail_Glassware_GlasswareId",
                table: "Cocktail",
                column: "GlasswareId",
                principalTable: "Glassware",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cocktail_Glassware_GlasswareId",
                table: "Cocktail");

            migrationBuilder.DropTable(
                name: "Glassware");

            migrationBuilder.DropIndex(
                name: "IX_Cocktail_GlasswareId",
                table: "Cocktail");

            migrationBuilder.DropColumn(
                name: "GlasswareId",
                table: "Cocktail");

            migrationBuilder.AddColumn<string>(
                name: "Glassware",
                table: "Cocktail",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
