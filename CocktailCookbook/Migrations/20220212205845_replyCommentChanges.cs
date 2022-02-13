using Microsoft.EntityFrameworkCore.Migrations;

namespace CocktailCookbook.Migrations
{
    public partial class replyCommentChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Comment");

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Comment",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comment_AuthorId",
                table: "Comment",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Staff_AuthorId",
                table: "Comment",
                column: "AuthorId",
                principalTable: "Staff",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Staff_AuthorId",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_AuthorId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Comment");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Comment",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
