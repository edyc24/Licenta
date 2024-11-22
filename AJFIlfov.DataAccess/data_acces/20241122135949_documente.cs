using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AJFIlfov.DataAccess.data_acces
{
    /// <inheritdoc />
    public partial class documente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Etapa",
                table: "Meciuri",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Locatie",
                table: "Meciuri",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ScorGazde",
                table: "Meciuri",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ScorOaspeti",
                table: "Meciuri",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImagineAnunt",
                table: "Anunturi",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeDocument = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PdfContent = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropColumn(
                name: "Etapa",
                table: "Meciuri");

            migrationBuilder.DropColumn(
                name: "Locatie",
                table: "Meciuri");

            migrationBuilder.DropColumn(
                name: "ScorGazde",
                table: "Meciuri");

            migrationBuilder.DropColumn(
                name: "ScorOaspeti",
                table: "Meciuri");

            migrationBuilder.DropColumn(
                name: "ImagineAnunt",
                table: "Anunturi");
        }
    }
}
