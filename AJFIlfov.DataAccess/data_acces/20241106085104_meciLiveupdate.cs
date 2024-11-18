using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AJFIlfov.DataAccess.data_acces
{
    /// <inheritdoc />
    public partial class meciLiveupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSecondHalf",
                table: "MeciuriLive",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Minut",
                table: "MeciuriLive",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "MeciuriLive",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSecondHalf",
                table: "MeciuriLive");

            migrationBuilder.DropColumn(
                name: "Minut",
                table: "MeciuriLive");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "MeciuriLive");
        }
    }
}
