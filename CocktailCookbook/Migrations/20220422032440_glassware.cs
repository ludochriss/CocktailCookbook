using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CocktailCookbook.Migrations
{
    public partial class glassware : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DailyTime",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "HourlyFrequency",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "RecursHourly",
                table: "Tasks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DailyTime",
                table: "Tasks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "HourlyFrequency",
                table: "Tasks",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "RecursHourly",
                table: "Tasks",
                type: "bit",
                nullable: true);
        }
    }
}
