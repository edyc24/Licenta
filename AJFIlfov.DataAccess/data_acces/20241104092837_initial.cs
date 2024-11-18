using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AJFIlfov.DataAccess.data_acces
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__GrupeEchi__IdEch__2A4B4B5E",
                table: "GrupeEchipa");

            migrationBuilder.DropForeignKey(
                name: "FK__GrupeEchi__IdGru__2B3F6F97",
                table: "GrupeEchipa");

            migrationBuilder.AddColumn<int>(
                name: "IdCategorie",
                table: "Utilizatori",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NumarTelefon",
                table: "Utilizatori",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ProfilePicture",
                table: "Utilizatori",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Utilizatori",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isSuspended",
                table: "Utilizatori",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataJoc",
                table: "Meciuri",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "idDeleted",
                table: "Meciuri",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Localitati",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdGrupa",
                table: "GrupeEchipa",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "IdEchipa",
                table: "GrupeEchipa",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "OraIncheiere",
                table: "Disponibilitate",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "OraIncepere",
                table: "Disponibilitate",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Categorii",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Categorie = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorii", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DisponibilitateAdmin",
                columns: table => new
                {
                    IdDisponibilitateAdmin = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Zi = table.Column<DateTime>(type: "date", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisponibilitateAdmin", x => x.IdDisponibilitateAdmin);
                });

            migrationBuilder.CreateTable(
                name: "PasswordRecovery",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    isAvailable = table.Column<bool>(type: "bit", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordRecovery", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PasswordRecovery_User",
                        column: x => x.UserID,
                        principalTable: "Utilizatori",
                        principalColumn: "IdUtilizator",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAddresses",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StreetAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserAddr__1788CC4C0BBEEBDD", x => x.UserId);
                    table.ForeignKey(
                        name: "FK__UserAddre__UserI__160F4887",
                        column: x => x.UserId,
                        principalTable: "Utilizatori",
                        principalColumn: "IdUtilizator");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Utilizatori_IdCategorie",
                table: "Utilizatori",
                column: "IdCategorie");

            migrationBuilder.CreateIndex(
                name: "IX_Utilizatori_IdCategorie1",
                table: "Utilizatori",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "IX_PasswordRecovery_UserID",
                table: "PasswordRecovery",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK__GrupeEchi__IdEch__2A4B4B5E",
                table: "GrupeEchipa",
                column: "IdEchipa",
                principalTable: "Echipe",
                principalColumn: "IdEchipa",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__GrupeEchi__IdGru__2B3F6F97",
                table: "GrupeEchipa",
                column: "IdGrupa",
                principalTable: "Grupe",
                principalColumn: "IdGrupa",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Utilizatori_Categorii",
                table: "Utilizatori",
                column: "IdCategorie",
                principalTable: "Categorii",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__GrupeEchi__IdEch__2A4B4B5E",
                table: "GrupeEchipa");

            migrationBuilder.DropForeignKey(
                name: "FK__GrupeEchi__IdGru__2B3F6F97",
                table: "GrupeEchipa");

            migrationBuilder.DropForeignKey(
                name: "FK_Utilizatori_Categorii",
                table: "Utilizatori");

            migrationBuilder.DropTable(
                name: "Categorii");

            migrationBuilder.DropTable(
                name: "DisponibilitateAdmin");

            migrationBuilder.DropTable(
                name: "PasswordRecovery");

            migrationBuilder.DropTable(
                name: "UserAddresses");

            migrationBuilder.DropIndex(
                name: "IX_Utilizatori_IdCategorie",
                table: "Utilizatori");

            migrationBuilder.DropIndex(
                name: "IX_Utilizatori_IdCategorie1",
                table: "Utilizatori");

            migrationBuilder.DropColumn(
                name: "IdCategorie",
                table: "Utilizatori");

            migrationBuilder.DropColumn(
                name: "NumarTelefon",
                table: "Utilizatori");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "Utilizatori");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Utilizatori");

            migrationBuilder.DropColumn(
                name: "isSuspended",
                table: "Utilizatori");

            migrationBuilder.DropColumn(
                name: "idDeleted",
                table: "Meciuri");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Localitati");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataJoc",
                table: "Meciuri",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "IdGrupa",
                table: "GrupeEchipa",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdEchipa",
                table: "GrupeEchipa",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "OraIncheiere",
                table: "Disponibilitate",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OraIncepere",
                table: "Disponibilitate",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK__GrupeEchi__IdEch__2A4B4B5E",
                table: "GrupeEchipa",
                column: "IdEchipa",
                principalTable: "Echipe",
                principalColumn: "IdEchipa");

            migrationBuilder.AddForeignKey(
                name: "FK__GrupeEchi__IdGru__2B3F6F97",
                table: "GrupeEchipa",
                column: "IdGrupa",
                principalTable: "Grupe",
                principalColumn: "IdGrupa");
        }
    }
}
