using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AJFIlfov.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class correctedappointments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "Time",
                table: "Appointment",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "Appointment");
        }
    }
}
