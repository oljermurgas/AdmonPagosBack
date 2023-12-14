using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminPagosApi.Migrations
{
    /// <inheritdoc />
    public partial class ActualizarCoordinador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JefeCoordinadorEmail",
                table: "CoordinacionPGNs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JefeCoordinadorNombre",
                table: "CoordinacionPGNs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JefeCoordinadorEmail",
                table: "CoordinacionPGNs");

            migrationBuilder.DropColumn(
                name: "JefeCoordinadorNombre",
                table: "CoordinacionPGNs");
        }
    }
}
