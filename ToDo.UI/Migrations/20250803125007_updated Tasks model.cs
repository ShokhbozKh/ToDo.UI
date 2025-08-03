using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDo.UI.Migrations
{
    /// <inheritdoc />
    public partial class updatedTasksmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinishedTime",
                table: "Tasks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeOnly>(
                name: "FinishedTime",
                table: "Tasks",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }
    }
}
