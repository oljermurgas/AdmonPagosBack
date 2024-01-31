using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminPagosApi.Migrations
{
    /// <inheritdoc />
    public partial class facturaUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlFactura",
                table: "FacturaRegistro");

            migrationBuilder.AddColumn<decimal>(
                name: "ValorFacturaxConcepto",
                table: "FacturaRegistro",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValorFacturaxConcepto",
                table: "FacturaRegistro");

            migrationBuilder.AddColumn<string>(
                name: "UrlFactura",
                table: "FacturaRegistro",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
