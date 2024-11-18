using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AJFIlfov.DataAccess.data_acces
{
    /// <inheritdoc />
    public partial class meciLive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MeciuriLive",
                columns: table => new
                {
                    IdMeciLive = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EchipaGazda = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EchipaOaspete = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScorGazda = table.Column<int>(type: "int", nullable: false),
                    ScorOaspete = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataOra = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeciuriLive", x => x.IdMeciLive);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MeciuriLive");
        }
    }
}
