using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminPagosApi.Migrations
{
    /// <inheritdoc />
    public partial class ActualizaEntidadTipoObligacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroContrato",
                table: "EntidadTipoObligaciones");

            migrationBuilder.DropColumn(
                name: "NumeroPagoElectronico",
                table: "EntidadTipoObligaciones");

            migrationBuilder.DropColumn(
                name: "PeriodicidadFactura",
                table: "EntidadTipoObligaciones");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NumeroContrato",
                table: "EntidadTipoObligaciones",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumeroPagoElectronico",
                table: "EntidadTipoObligaciones",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PeriodicidadFactura",
                table: "EntidadTipoObligaciones",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
