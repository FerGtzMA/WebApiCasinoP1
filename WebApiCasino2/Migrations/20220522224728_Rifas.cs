using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiCasino2.Migrations
{
    public partial class Rifas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rifa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rifa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonaRifa",
                columns: table => new
                {
                    PersonaId = table.Column<int>(type: "int", nullable: false),
                    RifaId = table.Column<int>(type: "int", nullable: false),
                    Oredn = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonaRifa", x => new { x.PersonaId, x.RifaId });
                    table.ForeignKey(
                        name: "FK_PersonaRifa_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonaRifa_Rifa_RifaId",
                        column: x => x.RifaId,
                        principalTable: "Rifa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonaRifa_RifaId",
                table: "PersonaRifa",
                column: "RifaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonaRifa");

            migrationBuilder.DropTable(
                name: "Rifa");
        }
    }
}
