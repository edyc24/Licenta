using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AJFIlfov.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class recover : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Echipe",
                columns: table => new
                {
                    IdEchipa = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nume = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Echipe__D4A3A318683D43A3", x => x.IdEchipa);
                });

            migrationBuilder.CreateTable(
                name: "Grupe",
                columns: table => new
                {
                    IdGrupa = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nume = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Grupe__303F6F275E4C9D6A", x => x.IdGrupa);
                });

            migrationBuilder.CreateTable(
                name: "Localitati",
                columns: table => new
                {
                    IdLocalitate = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nume = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Localita__937B022C50107212", x => x.IdLocalitate);
                });

            migrationBuilder.CreateTable(
                name: "Marimi",
                columns: table => new
                {
                    IdMarime = table.Column<int>(type: "int", nullable: false),
                    Marime = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Tip = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Marimi__95BA145B80EFB053", x => x.IdMarime);
                });

            migrationBuilder.CreateTable(
                name: "refereestats",
                columns: table => new
                {
                    varsta_medie = table.Column<int>(type: "int", nullable: true),
                    total = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Roluri",
                columns: table => new
                {
                    IdRol = table.Column<int>(type: "int", nullable: false),
                    Nume = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Roluri__2A49584C6CB95133", x => x.IdRol);
                });

            migrationBuilder.CreateTable(
                name: "Stadioane",
                columns: table => new
                {
                    IdStadion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nume = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Stadioan__6A3F1A8882EFD894", x => x.IdStadion);
                });

            migrationBuilder.CreateTable(
                name: "GrupeEchipa",
                columns: table => new
                {
                    IdGrupaEchipa = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdEchipa = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdGrupa = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__GrupeEch__F01D7F8F07F92D34", x => x.IdGrupaEchipa);
                    table.ForeignKey(
                        name: "FK__GrupeEchi__IdEch__2A4B4B5E",
                        column: x => x.IdEchipa,
                        principalTable: "Echipe",
                        principalColumn: "IdEchipa");
                    table.ForeignKey(
                        name: "FK__GrupeEchi__IdGru__2B3F6F97",
                        column: x => x.IdGrupa,
                        principalTable: "Grupe",
                        principalColumn: "IdGrupa");
                });

            migrationBuilder.CreateTable(
                name: "Utilizatori",
                columns: table => new
                {
                    IdUtilizator = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nume = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Prenume = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Mail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Parola = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DataIncepere = table.Column<DateTime>(type: "date", nullable: true),
                    DataNastere = table.Column<DateTime>(type: "date", nullable: true),
                    IdRol = table.Column<int>(type: "int", nullable: false),
                    IdMarimeAdidasi = table.Column<int>(type: "int", nullable: true),
                    IdMarimeHaine = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Utilizat__99101D6D186B6C92", x => x.IdUtilizator);
                    table.ForeignKey(
                        name: "FK__Utilizato__IdMar__34C8D9D1",
                        column: x => x.IdMarimeAdidasi,
                        principalTable: "Marimi",
                        principalColumn: "IdMarime");
                    table.ForeignKey(
                        name: "FK__Utilizato__IdMar__35BCFE0A",
                        column: x => x.IdMarimeHaine,
                        principalTable: "Marimi",
                        principalColumn: "IdMarime");
                    table.ForeignKey(
                        name: "FK__Utilizato__IdRol__33D4B598",
                        column: x => x.IdRol,
                        principalTable: "Roluri",
                        principalColumn: "IdRol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StadionLocalitate",
                columns: table => new
                {
                    IdStadionLocalitate = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdStadion = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdLocalitate = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__StadionL__8D363E8182F8F13E", x => x.IdStadionLocalitate);
                    table.ForeignKey(
                        name: "FK__StadionLo__IdLoc__3C69FB99",
                        column: x => x.IdLocalitate,
                        principalTable: "Localitati",
                        principalColumn: "IdLocalitate");
                    table.ForeignKey(
                        name: "FK__StadionLo__IdSta__3B75D760",
                        column: x => x.IdStadion,
                        principalTable: "Stadioane",
                        principalColumn: "IdStadion");
                });

            migrationBuilder.CreateTable(
                name: "Disponibilitate",
                columns: table => new
                {
                    IdDisponibilitate = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUtilizator = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Zi = table.Column<DateTime>(type: "date", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    OraIncepere = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    OraIncheiere = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Disponib__A79EDADBA5F65FC0", x => x.IdDisponibilitate);
                    table.ForeignKey(
                        name: "FK__Disponibi__IdUti__38996AB5",
                        column: x => x.IdUtilizator,
                        principalTable: "Utilizatori",
                        principalColumn: "IdUtilizator");
                });

            migrationBuilder.CreateTable(
                name: "Meciuri",
                columns: table => new
                {
                    IdMeci = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdEchipaGazda = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdEchipaOaspete = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DataJoc = table.Column<DateTime>(type: "date", nullable: true),
                    Rezultat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Observatii = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Raport = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    IdArbitru = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdArbitruAsistent1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdArbitruAsistent2 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdArbitruRezerva = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdObservator = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdStadionLocalitate = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Meciuri__4D7C0B75D32C1A8D", x => x.IdMeci);
                    table.ForeignKey(
                        name: "FK__Meciuri__IdArbit__4222D4EF",
                        column: x => x.IdArbitru,
                        principalTable: "Utilizatori",
                        principalColumn: "IdUtilizator");
                    table.ForeignKey(
                        name: "FK__Meciuri__IdArbit__4316F928",
                        column: x => x.IdArbitruAsistent1,
                        principalTable: "Utilizatori",
                        principalColumn: "IdUtilizator");
                    table.ForeignKey(
                        name: "FK__Meciuri__IdArbit__440B1D61",
                        column: x => x.IdArbitruAsistent2,
                        principalTable: "Utilizatori",
                        principalColumn: "IdUtilizator");
                    table.ForeignKey(
                        name: "FK__Meciuri__IdArbit__44FF419A",
                        column: x => x.IdArbitruRezerva,
                        principalTable: "Utilizatori",
                        principalColumn: "IdUtilizator");
                    table.ForeignKey(
                        name: "FK__Meciuri__IdEchip__403A8C7D",
                        column: x => x.IdEchipaGazda,
                        principalTable: "GrupeEchipa",
                        principalColumn: "IdGrupaEchipa");
                    table.ForeignKey(
                        name: "FK__Meciuri__IdEchip__412EB0B6",
                        column: x => x.IdEchipaOaspete,
                        principalTable: "GrupeEchipa",
                        principalColumn: "IdGrupaEchipa");
                    table.ForeignKey(
                        name: "FK__Meciuri__IdObser__45F365D3",
                        column: x => x.IdObservator,
                        principalTable: "Utilizatori",
                        principalColumn: "IdUtilizator");
                    table.ForeignKey(
                        name: "FK__Meciuri__IdStadi__46E78A0C",
                        column: x => x.IdStadionLocalitate,
                        principalTable: "StadionLocalitate",
                        principalColumn: "IdStadionLocalitate");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Disponibilitate_IdUtilizator",
                table: "Disponibilitate",
                column: "IdUtilizator");

            migrationBuilder.CreateIndex(
                name: "IX_GrupeEchipa_IdEchipa",
                table: "GrupeEchipa",
                column: "IdEchipa");

            migrationBuilder.CreateIndex(
                name: "IX_GrupeEchipa_IdGrupa",
                table: "GrupeEchipa",
                column: "IdGrupa");

            migrationBuilder.CreateIndex(
                name: "IX_Meciuri_IdArbitru",
                table: "Meciuri",
                column: "IdArbitru");

            migrationBuilder.CreateIndex(
                name: "IX_Meciuri_IdArbitruAsistent1",
                table: "Meciuri",
                column: "IdArbitruAsistent1");

            migrationBuilder.CreateIndex(
                name: "IX_Meciuri_IdArbitruAsistent2",
                table: "Meciuri",
                column: "IdArbitruAsistent2");

            migrationBuilder.CreateIndex(
                name: "IX_Meciuri_IdArbitruRezerva",
                table: "Meciuri",
                column: "IdArbitruRezerva");

            migrationBuilder.CreateIndex(
                name: "IX_Meciuri_IdEchipaGazda",
                table: "Meciuri",
                column: "IdEchipaGazda");

            migrationBuilder.CreateIndex(
                name: "IX_Meciuri_IdEchipaOaspete",
                table: "Meciuri",
                column: "IdEchipaOaspete");

            migrationBuilder.CreateIndex(
                name: "IX_Meciuri_IdObservator",
                table: "Meciuri",
                column: "IdObservator");

            migrationBuilder.CreateIndex(
                name: "IX_Meciuri_IdStadionLocalitate",
                table: "Meciuri",
                column: "IdStadionLocalitate");

            migrationBuilder.CreateIndex(
                name: "IX_StadionLocalitate_IdLocalitate",
                table: "StadionLocalitate",
                column: "IdLocalitate");

            migrationBuilder.CreateIndex(
                name: "IX_StadionLocalitate_IdStadion",
                table: "StadionLocalitate",
                column: "IdStadion");

            migrationBuilder.CreateIndex(
                name: "IX_Utilizatori_IdMarimeAdidasi",
                table: "Utilizatori",
                column: "IdMarimeAdidasi");

            migrationBuilder.CreateIndex(
                name: "IX_Utilizatori_IdMarimeHaine",
                table: "Utilizatori",
                column: "IdMarimeHaine");

            migrationBuilder.CreateIndex(
                name: "IX_Utilizatori_IdRol",
                table: "Utilizatori",
                column: "IdRol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Disponibilitate");

            migrationBuilder.DropTable(
                name: "Meciuri");

            migrationBuilder.DropTable(
                name: "refereestats");

            migrationBuilder.DropTable(
                name: "Utilizatori");

            migrationBuilder.DropTable(
                name: "GrupeEchipa");

            migrationBuilder.DropTable(
                name: "StadionLocalitate");

            migrationBuilder.DropTable(
                name: "Marimi");

            migrationBuilder.DropTable(
                name: "Roluri");

            migrationBuilder.DropTable(
                name: "Echipe");

            migrationBuilder.DropTable(
                name: "Grupe");

            migrationBuilder.DropTable(
                name: "Localitati");

            migrationBuilder.DropTable(
                name: "Stadioane");
        }
    }
}
