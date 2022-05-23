using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiCasino2.Migrations
{
    public partial class SemiComplete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "NumerosL",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NumerosL_UsuarioId",
                table: "NumerosL",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_NumerosL_AspNetUsers_UsuarioId",
                table: "NumerosL",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NumerosL_AspNetUsers_UsuarioId",
                table: "NumerosL");

            migrationBuilder.DropIndex(
                name: "IX_NumerosL_UsuarioId",
                table: "NumerosL");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "NumerosL");
        }
    }
}
