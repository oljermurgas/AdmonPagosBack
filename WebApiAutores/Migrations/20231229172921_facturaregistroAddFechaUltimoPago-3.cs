using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminPagosApi.Migrations
{
    /// <inheritdoc />
    public partial class facturaregistroAddFechaUltimoPago3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FechFacturaUltimoPago",
                table: "FacturaRegistro",
                newName: "FechaFacturaUltimoPago");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FechaFacturaUltimoPago",
                table: "FacturaRegistro",
                newName: "FechFacturaUltimoPago");
        }
    }
}
