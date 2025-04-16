using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AJFIlfov.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class modifyappointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Utilizatori_UserId",
                table: "Appointment");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_UserId",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Appointment");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Appointment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Appointment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "UtilizatoriIdUtilizator",
                table: "Appointment",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_UtilizatoriIdUtilizator",
                table: "Appointment",
                column: "UtilizatoriIdUtilizator");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Utilizatori_UtilizatoriIdUtilizator",
                table: "Appointment",
                column: "UtilizatoriIdUtilizator",
                principalTable: "Utilizatori",
                principalColumn: "IdUtilizator");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Utilizatori_UtilizatoriIdUtilizator",
                table: "Appointment");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_UtilizatoriIdUtilizator",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "UtilizatoriIdUtilizator",
                table: "Appointment");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Appointment",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_UserId",
                table: "Appointment",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Utilizatori_UserId",
                table: "Appointment",
                column: "UserId",
                principalTable: "Utilizatori",
                principalColumn: "IdUtilizator",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
