using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AJFIlfov.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Turnee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EchipeIdEchipa",
                table: "Meciuri",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EchipeIdEchipa1",
                table: "Meciuri",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CategoriiTurneu",
                columns: table => new
                {
                    IdCategorie = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Nume = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descriere = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IdDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriiTurneu", x => x.IdCategorie);
                });

            migrationBuilder.CreateTable(
                name: "Turnee",
                columns: table => new
                {
                    IdTurneu = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdEchipaGazda = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdEchipaOaspete = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdStadion = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdCategorie = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdGrupa = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ScorGazda = table.Column<int>(type: "int", nullable: true),
                    ScorOaspeti = table.Column<int>(type: "int", nullable: true),
                    IdDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turnee", x => x.IdTurneu);
                    table.ForeignKey(
                        name: "FK_Turnee_CategoriiTurneu",
                        column: x => x.IdCategorie,
                        principalTable: "CategoriiTurneu",
                        principalColumn: "IdCategorie");
                    table.ForeignKey(
                        name: "FK_Turnee_Echipe_Gazda",
                        column: x => x.IdEchipaGazda,
                        principalTable: "Echipe",
                        principalColumn: "IdEchipa");
                    table.ForeignKey(
                        name: "FK_Turnee_Echipe_Oaspete",
                        column: x => x.IdEchipaOaspete,
                        principalTable: "Echipe",
                        principalColumn: "IdEchipa");
                    table.ForeignKey(
                        name: "FK_Turnee_Grupe",
                        column: x => x.IdGrupa,
                        principalTable: "Grupe",
                        principalColumn: "IdGrupa");
                    table.ForeignKey(
                        name: "FK_Turnee_Stadioane",
                        column: x => x.IdStadion,
                        principalTable: "Stadioane",
                        principalColumn: "IdStadion");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meciuri_EchipeIdEchipa",
                table: "Meciuri",
                column: "EchipeIdEchipa");

            migrationBuilder.CreateIndex(
                name: "IX_Meciuri_EchipeIdEchipa1",
                table: "Meciuri",
                column: "EchipeIdEchipa1");

            migrationBuilder.CreateIndex(
                name: "IX_Turnee_IdCategorie",
                table: "Turnee",
                column: "IdCategorie");

            migrationBuilder.CreateIndex(
                name: "IX_Turnee_IdEchipaGazda",
                table: "Turnee",
                column: "IdEchipaGazda");

            migrationBuilder.CreateIndex(
                name: "IX_Turnee_IdEchipaOaspete",
                table: "Turnee",
                column: "IdEchipaOaspete");

            migrationBuilder.CreateIndex(
                name: "IX_Turnee_IdGrupa",
                table: "Turnee",
                column: "IdGrupa");

            migrationBuilder.CreateIndex(
                name: "IX_Turnee_IdStadion",
                table: "Turnee",
                column: "IdStadion");

            migrationBuilder.AddForeignKey(
                name: "FK_Meciuri_Echipe_EchipeIdEchipa",
                table: "Meciuri",
                column: "EchipeIdEchipa",
                principalTable: "Echipe",
                principalColumn: "IdEchipa");

            migrationBuilder.AddForeignKey(
                name: "FK_Meciuri_Echipe_EchipeIdEchipa1",
                table: "Meciuri",
                column: "EchipeIdEchipa1",
                principalTable: "Echipe",
                principalColumn: "IdEchipa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meciuri_Echipe_EchipeIdEchipa",
                table: "Meciuri");

            migrationBuilder.DropForeignKey(
                name: "FK_Meciuri_Echipe_EchipeIdEchipa1",
                table: "Meciuri");

            migrationBuilder.DropTable(
                name: "Turnee");

            migrationBuilder.DropTable(
                name: "CategoriiTurneu");

            migrationBuilder.DropIndex(
                name: "IX_Meciuri_EchipeIdEchipa",
                table: "Meciuri");

            migrationBuilder.DropIndex(
                name: "IX_Meciuri_EchipeIdEchipa1",
                table: "Meciuri");

            migrationBuilder.DropColumn(
                name: "EchipeIdEchipa",
                table: "Meciuri");

            migrationBuilder.DropColumn(
                name: "EchipeIdEchipa1",
                table: "Meciuri");
        }
    }
}
