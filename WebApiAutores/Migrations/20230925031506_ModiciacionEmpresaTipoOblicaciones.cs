using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminPagosApi.Migrations
{
    /// <inheritdoc />
    public partial class ModiciacionEmpresaTipoOblicaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TerceroIdentificacion",
                table: "EntidadTipoObligaciones",
                newName: "NumeroPagoElectronico");

            migrationBuilder.AddColumn<string>(
                name: "NumeroContrato",
                table: "EntidadTipoObligaciones",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "periodicidadFactura",
                table: "EntidadTipoObligaciones",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroContrato",
                table: "EntidadTipoObligaciones");

            migrationBuilder.DropColumn(
                name: "periodicidadFactura",
                table: "EntidadTipoObligaciones");

            migrationBuilder.RenameColumn(
                name: "NumeroPagoElectronico",
                table: "EntidadTipoObligaciones",
                newName: "TerceroIdentificacion");
        }
    }
}
