using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminPagosApi.Migrations
{
    /// <inheritdoc />
    public partial class TerceroActualizar02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LinkSecop",
                table: "SedeContratos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Meses",
                table: "SedeContratos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Valor",
                table: "SedeContratos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LinkSecop",
                table: "SedeContratos");

            migrationBuilder.DropColumn(
                name: "Meses",
                table: "SedeContratos");

            migrationBuilder.DropColumn(
                name: "Valor",
                table: "SedeContratos");
        }
    }
}
