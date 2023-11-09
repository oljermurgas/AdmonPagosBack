using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminPagosApi.Migrations
{
    /// <inheritdoc />
    public partial class ModificaFacturaTipoObligacionImagen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "imagen",
                table: "TipoObligacion",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imagen",
                table: "TipoObligacion");
        }
    }
}
