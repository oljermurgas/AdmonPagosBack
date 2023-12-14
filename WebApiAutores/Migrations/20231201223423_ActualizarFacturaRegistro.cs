using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminPagosApi.Migrations
{
    /// <inheritdoc />
    public partial class ActualizarFacturaRegistro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FechaUltimoPago",
                table: "FacturaRegistro",
                newName: "FechaPeriodoFacturaInicio");

            migrationBuilder.RenameColumn(
                name: "FechaProximaFecha",
                table: "FacturaRegistro",
                newName: "FechaPeriodoFacturaFin");

            migrationBuilder.RenameColumn(
                name: "FechaPago",
                table: "FacturaRegistro",
                newName: "FechaOportunoPago");

            migrationBuilder.AddColumn<bool>(
                name: "PagoInmediato",
                table: "FacturaRegistro",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PagoInmediato",
                table: "FacturaRegistro");

            migrationBuilder.RenameColumn(
                name: "FechaPeriodoFacturaInicio",
                table: "FacturaRegistro",
                newName: "FechaUltimoPago");

            migrationBuilder.RenameColumn(
                name: "FechaPeriodoFacturaFin",
                table: "FacturaRegistro",
                newName: "FechaProximaFecha");

            migrationBuilder.RenameColumn(
                name: "FechaOportunoPago",
                table: "FacturaRegistro",
                newName: "FechaPago");
        }
    }
}
