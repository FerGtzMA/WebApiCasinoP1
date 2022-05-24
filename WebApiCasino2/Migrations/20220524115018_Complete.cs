using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiCasino2.Migrations
{
    public partial class Complete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonaRifa_Personas_PersonaId",
                table: "PersonaRifa");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonaRifa_Rifa_RifaId",
                table: "PersonaRifa");

            migrationBuilder.DropTable(
                name: "NumerosL");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rifa",
                table: "Rifa");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonaRifa",
                table: "PersonaRifa");

            migrationBuilder.RenameTable(
                name: "Rifa",
                newName: "Rifas");

            migrationBuilder.RenameTable(
                name: "PersonaRifa",
                newName: "PersonasRifas");

            migrationBuilder.RenameColumn(
                name: "Oredn",
                table: "PersonasRifas",
                newName: "PremioId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonaRifa_RifaId",
                table: "PersonasRifas",
                newName: "IX_PersonasRifas_RifaId");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Rifas",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "existencias",
                table: "Rifas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Gana",
                table: "PersonasRifas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PersonasRifas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumLoteria",
                table: "PersonasRifas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rifas",
                table: "Rifas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonasRifas",
                table: "PersonasRifas",
                columns: new[] { "PersonaId", "RifaId" });

            migrationBuilder.CreateTable(
                name: "Premios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Orden = table.Column<int>(type: "int", nullable: false),
                    Entregado = table.Column<bool>(type: "bit", nullable: false),
                    RifaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Premios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Premios_Rifas_RifaId",
                        column: x => x.RifaId,
                        principalTable: "Rifas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Premios_RifaId",
                table: "Premios",
                column: "RifaId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonasRifas_Personas_PersonaId",
                table: "PersonasRifas",
                column: "PersonaId",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonasRifas_Rifas_RifaId",
                table: "PersonasRifas",
                column: "RifaId",
                principalTable: "Rifas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonasRifas_Personas_PersonaId",
                table: "PersonasRifas");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonasRifas_Rifas_RifaId",
                table: "PersonasRifas");

            migrationBuilder.DropTable(
                name: "Premios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rifas",
                table: "Rifas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonasRifas",
                table: "PersonasRifas");

            migrationBuilder.DropColumn(
                name: "existencias",
                table: "Rifas");

            migrationBuilder.DropColumn(
                name: "Gana",
                table: "PersonasRifas");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PersonasRifas");

            migrationBuilder.DropColumn(
                name: "NumLoteria",
                table: "PersonasRifas");

            migrationBuilder.RenameTable(
                name: "Rifas",
                newName: "Rifa");

            migrationBuilder.RenameTable(
                name: "PersonasRifas",
                newName: "PersonaRifa");

            migrationBuilder.RenameColumn(
                name: "PremioId",
                table: "PersonaRifa",
                newName: "Oredn");

            migrationBuilder.RenameIndex(
                name: "IX_PersonasRifas_RifaId",
                table: "PersonaRifa",
                newName: "IX_PersonaRifa_RifaId");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Rifa",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rifa",
                table: "Rifa",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonaRifa",
                table: "PersonaRifa",
                columns: new[] { "PersonaId", "RifaId" });

            migrationBuilder.CreateTable(
                name: "NumerosL",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RifaId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Vendido = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumerosL", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NumerosL_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NumerosL_Rifa_RifaId",
                        column: x => x.RifaId,
                        principalTable: "Rifa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NumerosL_RifaId",
                table: "NumerosL",
                column: "RifaId");

            migrationBuilder.CreateIndex(
                name: "IX_NumerosL_UsuarioId",
                table: "NumerosL",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonaRifa_Personas_PersonaId",
                table: "PersonaRifa",
                column: "PersonaId",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonaRifa_Rifa_RifaId",
                table: "PersonaRifa",
                column: "RifaId",
                principalTable: "Rifa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
