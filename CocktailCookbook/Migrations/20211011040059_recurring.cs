using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CocktailCookbook.Migrations
{
    public partial class recurring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Frequency",
                table: "Tasks");

            migrationBuilder.AddColumn<DateTime>(
                name: "DailyTime",
                table: "Tasks",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "HourlyFrequency",
                table: "Tasks",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "RecursDaily",
                table: "Tasks",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "RecursHourly",
                table: "Tasks",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "RecursWeekly",
                table: "Tasks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DailyTime",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "HourlyFrequency",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "RecursDaily",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "RecursHourly",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "RecursWeekly",
                table: "Tasks");

            migrationBuilder.AddColumn<string>(
                name: "Frequency",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
