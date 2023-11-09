using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminPagosApi.Migrations
{
    /// <inheritdoc />
    public partial class TerceroActualizar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Municipio_Departamentos_DepartamentoId",
                table: "Municipio");

            migrationBuilder.DropIndex(
                name: "IX_Municipio_DepartamentoId",
                table: "Municipio");

            migrationBuilder.DropColumn(
                name: "DepartamentoId",
                table: "Municipio");

            migrationBuilder.AddColumn<string>(
                name: "TerceroApellidos",
                table: "SedeContratos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Municipio_CodDep",
                table: "Municipio",
                column: "CodDep");

            migrationBuilder.AddForeignKey(
                name: "FK_Municipio_Departamentos_CodDep",
                table: "Municipio",
                column: "CodDep",
                principalTable: "Departamentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Municipio_Departamentos_CodDep",
                table: "Municipio");

            migrationBuilder.DropIndex(
                name: "IX_Municipio_CodDep",
                table: "Municipio");

            migrationBuilder.DropColumn(
                name: "TerceroApellidos",
                table: "SedeContratos");

            migrationBuilder.AddColumn<int>(
                name: "DepartamentoId",
                table: "Municipio",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Municipio_DepartamentoId",
                table: "Municipio",
                column: "DepartamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Municipio_Departamentos_DepartamentoId",
                table: "Municipio",
                column: "DepartamentoId",
                principalTable: "Departamentos",
                principalColumn: "Id");
        }
    }
}
