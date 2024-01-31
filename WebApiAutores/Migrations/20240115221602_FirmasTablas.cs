using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminPagosApi.Migrations
{
    /// <inheritdoc />
    public partial class FirmasTablas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Firmas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FuncionarioElaboro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FuncionarioElaboroCargo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FuncionarioElaboroDependencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FuncionarioElaboroFirma = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    FuncionarioAprueba = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FuncionarioApruebaCargo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FuncionarioApruebaDependencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FuncionarioApruebaFirma = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoPagoAdmonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Firmas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Firmas_TipoPagoAdmon_TipoPagoAdmonId",
                        column: x => x.TipoPagoAdmonId,
                        principalTable: "TipoPagoAdmon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Firmas_TipoPagoAdmonId",
                table: "Firmas",
                column: "TipoPagoAdmonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Firmas");
        }
    }
}
