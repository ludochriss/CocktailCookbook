using Microsoft.EntityFrameworkCore.Migrations;

namespace CocktailCookbook.Migrations
{
    public partial class deptchange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Department",
                table: "Tasks");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Tasks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_DepartmentId",
                table: "Tasks",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Departments_DepartmentId",
                table: "Tasks",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Departments_DepartmentId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_DepartmentId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Tasks");

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
