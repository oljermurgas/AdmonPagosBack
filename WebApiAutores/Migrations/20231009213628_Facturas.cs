using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminPagosApi.Migrations
{
    /// <inheritdoc />
    public partial class Facturas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Meses",
                table: "SedeContratos");

            migrationBuilder.DropColumn(
                name: "TerceroApellidos",
                table: "SedeContratos");

            migrationBuilder.RenameColumn(
                name: "TerceroNombres",
                table: "SedeContratos",
                newName: "RazonSocial");

            migrationBuilder.RenameColumn(
                name: "TerceroIdentificacion",
                table: "SedeContratos",
                newName: "Identificacion");

            migrationBuilder.RenameColumn(
                name: "periodicidadFactura",
                table: "EntidadTipoObligaciones",
                newName: "PeriodicidadFactura");

            migrationBuilder.CreateTable(
                name: "FacturaEstado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ColorLetra = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColorFondo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacturaEstado", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FacturaRegistro",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SedeId = table.Column<int>(type: "int", nullable: false),
                    EntidadId = table.Column<int>(type: "int", nullable: false),
                    NumeroContrato = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacturaNumero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferenciaPago = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaEmision = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaPago = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValorFactura = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FacturaEstadoId = table.Column<int>(type: "int", nullable: false),
                    Nota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaUltimoPago = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaProximaFecha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValorFacturaUltimoPago = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UrlFactura = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacturaRegistro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacturaRegistro_Entidades_EntidadId",
                        column: x => x.EntidadId,
                        principalTable: "Entidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FacturaRegistro_FacturaEstado_FacturaEstadoId",
                        column: x => x.FacturaEstadoId,
                        principalTable: "FacturaEstado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FacturaRegistro_Sedes_SedeId",
                        column: x => x.SedeId,
                        principalTable: "Sedes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "FacturaTipoObligacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacturaRegistroId = table.Column<int>(type: "int", nullable: false),
                    TipoObligacionId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacturaTipoObligacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacturaTipoObligacion_FacturaRegistro_FacturaRegistroId",
                        column: x => x.FacturaRegistroId,
                        principalTable: "FacturaRegistro",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_FacturaTipoObligacion_TipoObligacion_TipoObligacionId",
                        column: x => x.TipoObligacionId,
                        principalTable: "TipoObligacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "FacturaTipoObligacionConceptos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacturaTipoObligacionId = table.Column<int>(type: "int", nullable: false),
                    TipoConceptoFacturacionId = table.Column<int>(type: "int", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Nota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacturaTipoObligacionConceptos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacturaTipoObligacionConceptos_FacturaTipoObligacion_FacturaTipoObligacionId",
                        column: x => x.FacturaTipoObligacionId,
                        principalTable: "FacturaTipoObligacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_FacturaTipoObligacionConceptos_TipoConceptoFacturacion_TipoConceptoFacturacionId",
                        column: x => x.TipoConceptoFacturacionId,
                        principalTable: "TipoConceptoFacturacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FacturaRegistro_EntidadId",
                table: "FacturaRegistro",
                column: "EntidadId");

            migrationBuilder.CreateIndex(
                name: "IX_FacturaRegistro_FacturaEstadoId",
                table: "FacturaRegistro",
                column: "FacturaEstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_FacturaRegistro_SedeId",
                table: "FacturaRegistro",
                column: "SedeId");

            migrationBuilder.CreateIndex(
                name: "IX_FacturaTipoObligacion_FacturaRegistroId",
                table: "FacturaTipoObligacion",
                column: "FacturaRegistroId");

            migrationBuilder.CreateIndex(
                name: "IX_FacturaTipoObligacion_TipoObligacionId",
                table: "FacturaTipoObligacion",
                column: "TipoObligacionId");

            migrationBuilder.CreateIndex(
                name: "IX_FacturaTipoObligacionConceptos_FacturaTipoObligacionId",
                table: "FacturaTipoObligacionConceptos",
                column: "FacturaTipoObligacionId");

            migrationBuilder.CreateIndex(
                name: "IX_FacturaTipoObligacionConceptos_TipoConceptoFacturacionId",
                table: "FacturaTipoObligacionConceptos",
                column: "TipoConceptoFacturacionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FacturaTipoObligacionConceptos");

            migrationBuilder.DropTable(
                name: "FacturaTipoObligacion");

            migrationBuilder.DropTable(
                name: "FacturaRegistro");

            migrationBuilder.DropTable(
                name: "FacturaEstado");

            migrationBuilder.RenameColumn(
                name: "RazonSocial",
                table: "SedeContratos",
                newName: "TerceroNombres");

            migrationBuilder.RenameColumn(
                name: "Identificacion",
                table: "SedeContratos",
                newName: "TerceroIdentificacion");

            migrationBuilder.RenameColumn(
                name: "PeriodicidadFactura",
                table: "EntidadTipoObligaciones",
                newName: "periodicidadFactura");

            migrationBuilder.AddColumn<int>(
                name: "Meses",
                table: "SedeContratos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TerceroApellidos",
                table: "SedeContratos",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
