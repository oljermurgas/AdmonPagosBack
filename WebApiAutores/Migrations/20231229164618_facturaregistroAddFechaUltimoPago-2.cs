using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminPagosApi.Migrations
{
    /// <inheritdoc />
    public partial class facturaregistroAddFechaUltimoPago2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FechFacturaUltimoPagon",
                table: "FacturaRegistro",
                newName: "FechFacturaUltimoPago");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FechFacturaUltimoPago",
                table: "FacturaRegistro",
                newName: "FechFacturaUltimoPagon");
        }
    }
}
