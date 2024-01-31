using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminPagosApi.Migrations
{
    /// <inheritdoc />
    public partial class CoordinadorAddUsuarioId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "CoordinacionPGNs",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CoordinacionPGNs_UsuarioId",
                table: "CoordinacionPGNs",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_CoordinacionPGNs_AspNetUsers_UsuarioId",
                table: "CoordinacionPGNs",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoordinacionPGNs_AspNetUsers_UsuarioId",
                table: "CoordinacionPGNs");

            migrationBuilder.DropIndex(
                name: "IX_CoordinacionPGNs_UsuarioId",
                table: "CoordinacionPGNs");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "CoordinacionPGNs");
        }
    }
}
