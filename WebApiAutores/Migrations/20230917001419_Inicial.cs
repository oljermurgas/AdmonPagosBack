using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminPagosApi.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CoordinacionPGNs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Coodinacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Responsable = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoordinacionPGNs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodDep = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MMenus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Menu = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    MenuId = table.Column<int>(type: "int", nullable: false),
                    Icono = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    TextoAdicional = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Link = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    Nivel = table.Column<int>(type: "int", nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    MMenuPadreId = table.Column<int>(type: "int", nullable: true),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MMenus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MMenus_MMenus_MMenuPadreId",
                        column: x => x.MMenuPadreId,
                        principalTable: "MMenus",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TipoCanalEnvioFactura",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoCanalEnvioFactura", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoEmpresa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoEmpresa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoEmpresaNivel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoEmpresaNivel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoEmpresaSector",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoEmpresaSector", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoInmueble",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoInmueble", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoPagoAdmon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPagoAdmon", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoTarifa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoTarifa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoVinculacionContractual",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoVinculacionContractual", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Libros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AutorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Libros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Libros_Autores_AutorId",
                        column: x => x.AutorId,
                        principalTable: "Autores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Municipio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodMun = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodDep = table.Column<int>(type: "int", nullable: false),
                    DepartamentoId = table.Column<int>(type: "int", nullable: true),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Municipio_Departamentos_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamentos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MMenuRoles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rol = table.Column<string>(type: "NVARCHAR(256)", nullable: false),
                    MMenuId = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MMenuRoles", x => x.id);
                    table.ForeignKey(
                        name: "FK_MMenuRoles_MMenus_MMenuId",
                        column: x => x.MMenuId,
                        principalTable: "MMenus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TipoConceptoFacturacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoPagoAdmonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoConceptoFacturacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TipoConceptoFacturacion_TipoPagoAdmon_TipoPagoAdmonId",
                        column: x => x.TipoPagoAdmonId,
                        principalTable: "TipoPagoAdmon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TipoObligacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoPagoAdmonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoObligacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TipoObligacion_TipoPagoAdmon_TipoPagoAdmonId",
                        column: x => x.TipoPagoAdmonId,
                        principalTable: "TipoPagoAdmon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Entidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Identificacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nivel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoEmpresaId = table.Column<int>(type: "int", nullable: false),
                    TipoEmpresaSectorId = table.Column<int>(type: "int", nullable: false),
                    TipoEmpresaNivelId = table.Column<int>(type: "int", nullable: false),
                    DepartamentoId = table.Column<int>(type: "int", nullable: false),
                    MunicipioId = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entidades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entidades_Departamentos_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Entidades_Municipio_MunicipioId",
                        column: x => x.MunicipioId,
                        principalTable: "Municipio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Entidades_TipoEmpresaNivel_TipoEmpresaNivelId",
                        column: x => x.TipoEmpresaNivelId,
                        principalTable: "TipoEmpresaNivel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Entidades_TipoEmpresaSector_TipoEmpresaSectorId",
                        column: x => x.TipoEmpresaSectorId,
                        principalTable: "TipoEmpresaSector",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Entidades_TipoEmpresa_TipoEmpresaId",
                        column: x => x.TipoEmpresaId,
                        principalTable: "TipoEmpresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sedes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentificacionHominis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CedulaCatastral = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatriculaInnoviliaria = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoVinculacionContractualId = table.Column<int>(type: "int", nullable: false),
                    TipoInmuebleId = table.Column<int>(type: "int", nullable: false),
                    DepartamentoId = table.Column<int>(type: "int", nullable: false),
                    MunicipioId = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sedes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sedes_Departamentos_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sedes_Municipio_MunicipioId",
                        column: x => x.MunicipioId,
                        principalTable: "Municipio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sedes_TipoInmueble_TipoInmuebleId",
                        column: x => x.TipoInmuebleId,
                        principalTable: "TipoInmueble",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sedes_TipoVinculacionContractual_TipoVinculacionContractualId",
                        column: x => x.TipoVinculacionContractualId,
                        principalTable: "TipoVinculacionContractual",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntidadTipoObligaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TerceroIdentificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    EntidadId = table.Column<int>(type: "int", nullable: false),
                    TipoObligacionId = table.Column<int>(type: "int", nullable: false),
                    TipoTarifaId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntidadTipoObligaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntidadTipoObligaciones_Entidades_EntidadId",
                        column: x => x.EntidadId,
                        principalTable: "Entidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntidadTipoObligaciones_TipoObligacion_TipoObligacionId",
                        column: x => x.TipoObligacionId,
                        principalTable: "TipoObligacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntidadTipoObligaciones_TipoTarifa_TipoTarifaId",
                        column: x => x.TipoTarifaId,
                        principalTable: "TipoTarifa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CoordinacionPGNSedes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoordinacionPGNId = table.Column<int>(type: "int", nullable: false),
                    SedeId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoordinacionPGNSedes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoordinacionPGNSedes_CoordinacionPGNs_CoordinacionPGNId",
                        column: x => x.CoordinacionPGNId,
                        principalTable: "CoordinacionPGNs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoordinacionPGNSedes_Sedes_SedeId",
                        column: x => x.SedeId,
                        principalTable: "Sedes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SedeContratos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentoNumero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaFinal = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TerceroIdentificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TerceroNombres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notas = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    SedeId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SedeContratos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SedeContratos_Sedes_SedeId",
                        column: x => x.SedeId,
                        principalTable: "Sedes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SedeEntidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Notas = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SedeId = table.Column<int>(type: "int", nullable: false),
                    EntidadId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SedeEntidades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SedeEntidades_Entidades_EntidadId",
                        column: x => x.EntidadId,
                        principalTable: "Entidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SedeEntidades_Sedes_SedeId",
                        column: x => x.SedeId,
                        principalTable: "Sedes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoordinacionPGNSedes_CoordinacionPGNId",
                table: "CoordinacionPGNSedes",
                column: "CoordinacionPGNId");

            migrationBuilder.CreateIndex(
                name: "IX_CoordinacionPGNSedes_SedeId",
                table: "CoordinacionPGNSedes",
                column: "SedeId");

            migrationBuilder.CreateIndex(
                name: "IX_Entidades_DepartamentoId",
                table: "Entidades",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Entidades_MunicipioId",
                table: "Entidades",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_Entidades_TipoEmpresaId",
                table: "Entidades",
                column: "TipoEmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Entidades_TipoEmpresaNivelId",
                table: "Entidades",
                column: "TipoEmpresaNivelId");

            migrationBuilder.CreateIndex(
                name: "IX_Entidades_TipoEmpresaSectorId",
                table: "Entidades",
                column: "TipoEmpresaSectorId");

            migrationBuilder.CreateIndex(
                name: "IX_EntidadTipoObligaciones_EntidadId",
                table: "EntidadTipoObligaciones",
                column: "EntidadId");

            migrationBuilder.CreateIndex(
                name: "IX_EntidadTipoObligaciones_TipoObligacionId",
                table: "EntidadTipoObligaciones",
                column: "TipoObligacionId");

            migrationBuilder.CreateIndex(
                name: "IX_EntidadTipoObligaciones_TipoTarifaId",
                table: "EntidadTipoObligaciones",
                column: "TipoTarifaId");

            migrationBuilder.CreateIndex(
                name: "IX_Libros_AutorId",
                table: "Libros",
                column: "AutorId");

            migrationBuilder.CreateIndex(
                name: "IX_MMenuRoles_MMenuId",
                table: "MMenuRoles",
                column: "MMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_MMenus_MMenuPadreId",
                table: "MMenus",
                column: "MMenuPadreId");

            migrationBuilder.CreateIndex(
                name: "IX_Municipio_DepartamentoId",
                table: "Municipio",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_SedeContratos_SedeId",
                table: "SedeContratos",
                column: "SedeId");

            migrationBuilder.CreateIndex(
                name: "IX_SedeEntidades_EntidadId",
                table: "SedeEntidades",
                column: "EntidadId");

            migrationBuilder.CreateIndex(
                name: "IX_SedeEntidades_SedeId",
                table: "SedeEntidades",
                column: "SedeId");

            migrationBuilder.CreateIndex(
                name: "IX_Sedes_DepartamentoId",
                table: "Sedes",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Sedes_MunicipioId",
                table: "Sedes",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_Sedes_TipoInmuebleId",
                table: "Sedes",
                column: "TipoInmuebleId");

            migrationBuilder.CreateIndex(
                name: "IX_Sedes_TipoVinculacionContractualId",
                table: "Sedes",
                column: "TipoVinculacionContractualId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoConceptoFacturacion_TipoPagoAdmonId",
                table: "TipoConceptoFacturacion",
                column: "TipoPagoAdmonId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoObligacion_TipoPagoAdmonId",
                table: "TipoObligacion",
                column: "TipoPagoAdmonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoordinacionPGNSedes");

            migrationBuilder.DropTable(
                name: "EntidadTipoObligaciones");

            migrationBuilder.DropTable(
                name: "Libros");

            migrationBuilder.DropTable(
                name: "MMenuRoles");

            migrationBuilder.DropTable(
                name: "SedeContratos");

            migrationBuilder.DropTable(
                name: "SedeEntidades");

            migrationBuilder.DropTable(
                name: "TipoCanalEnvioFactura");

            migrationBuilder.DropTable(
                name: "TipoConceptoFacturacion");

            migrationBuilder.DropTable(
                name: "CoordinacionPGNs");

            migrationBuilder.DropTable(
                name: "TipoObligacion");

            migrationBuilder.DropTable(
                name: "TipoTarifa");

            migrationBuilder.DropTable(
                name: "Autores");

            migrationBuilder.DropTable(
                name: "MMenus");

            migrationBuilder.DropTable(
                name: "Entidades");

            migrationBuilder.DropTable(
                name: "Sedes");

            migrationBuilder.DropTable(
                name: "TipoPagoAdmon");

            migrationBuilder.DropTable(
                name: "TipoEmpresaNivel");

            migrationBuilder.DropTable(
                name: "TipoEmpresaSector");

            migrationBuilder.DropTable(
                name: "TipoEmpresa");

            migrationBuilder.DropTable(
                name: "Municipio");

            migrationBuilder.DropTable(
                name: "TipoInmueble");

            migrationBuilder.DropTable(
                name: "TipoVinculacionContractual");

            migrationBuilder.DropTable(
                name: "Departamentos");
        }
    }
}
