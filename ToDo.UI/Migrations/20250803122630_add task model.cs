using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDo.UI.Migrations
{
    /// <inheritdoc />
    public partial class addtaskmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "ToDoStatus",
                table: "Tasks",
                newName: "TaskStatus");

            migrationBuilder.AddColumn<int>(
                name: "DurationInMinutes",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "FinishedTime",
                table: "Tasks",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationInMinutes",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "FinishedTime",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "TaskStatus",
                table: "Tasks",
                newName: "ToDoStatus");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Tasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
