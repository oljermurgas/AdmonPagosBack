IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Autores] (
    [Id] int NOT NULL IDENTITY,
    [Nombre] nvarchar(max) NULL,
    CONSTRAINT [PK_Autores] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [CoordinacionPGNs] (
    [Id] int NOT NULL IDENTITY,
    [Coodinacion] nvarchar(max) NULL,
    [Responsable] nvarchar(max) NULL,
    [Direccion] nvarchar(max) NULL,
    [email] nvarchar(max) NULL,
    [Telefono] nvarchar(max) NULL,
    [UsuarioId] int NOT NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [FechaModificacion] datetime2 NOT NULL,
    CONSTRAINT [PK_CoordinacionPGNs] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Departamentos] (
    [Id] int NOT NULL IDENTITY,
    [CodDep] int NOT NULL,
    [Nombre] nvarchar(max) NULL,
    [UsuarioId] int NOT NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [FechaModificacion] datetime2 NOT NULL,
    CONSTRAINT [PK_Departamentos] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [MMenus] (
    [Id] int NOT NULL IDENTITY,
    [Menu] nvarchar(30) NOT NULL,
    [MenuId] int NOT NULL,
    [Icono] nvarchar(60) NULL,
    [TextoAdicional] nvarchar(30) NULL,
    [Link] nvarchar(60) NULL,
    [Activo] bit NOT NULL,
    [Nivel] int NOT NULL,
    [Orden] int NOT NULL,
    [UserName] nvarchar(40) NULL,
    [MMenuPadreId] int NULL,
    [UsuarioId] int NOT NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [FechaModificacion] datetime2 NOT NULL,
    CONSTRAINT [PK_MMenus] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_MMenus_MMenus_MMenuPadreId] FOREIGN KEY ([MMenuPadreId]) REFERENCES [MMenus] ([Id])
);
GO

CREATE TABLE [TipoCanalEnvioFactura] (
    [Id] int NOT NULL IDENTITY,
    [Codigo] nvarchar(max) NOT NULL,
    [Descripcion] nvarchar(max) NOT NULL,
    [UsuarioId] int NOT NULL,
    [Estado] bit NOT NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [FechaModificacion] datetime2 NOT NULL,
    CONSTRAINT [PK_TipoCanalEnvioFactura] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [TipoEmpresa] (
    [Id] int NOT NULL IDENTITY,
    [Codigo] nvarchar(max) NULL,
    [Descripcion] nvarchar(max) NOT NULL,
    [Estado] bit NOT NULL,
    [UsuarioId] int NOT NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [FechaModificacion] datetime2 NOT NULL,
    CONSTRAINT [PK_TipoEmpresa] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [TipoEmpresaNivel] (
    [Id] int NOT NULL IDENTITY,
    [Codigo] nvarchar(max) NULL,
    [Descripcion] nvarchar(max) NOT NULL,
    [Estado] bit NOT NULL,
    [UsuarioId] int NOT NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [FechaModificacion] datetime2 NOT NULL,
    CONSTRAINT [PK_TipoEmpresaNivel] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [TipoEmpresaSector] (
    [Id] int NOT NULL IDENTITY,
    [Codigo] nvarchar(30) NOT NULL,
    [Descripcion] nvarchar(max) NOT NULL,
    [Estado] bit NOT NULL,
    [UsuarioId] int NOT NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [FechaModificacion] datetime2 NOT NULL,
    CONSTRAINT [PK_TipoEmpresaSector] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [TipoInmueble] (
    [Id] int NOT NULL IDENTITY,
    [Codigo] nvarchar(30) NOT NULL,
    [Descripcion] nvarchar(max) NOT NULL,
    [Estado] bit NOT NULL,
    [UsuarioId] int NOT NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [FechaModificacion] datetime2 NOT NULL,
    CONSTRAINT [PK_TipoInmueble] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [TipoPagoAdmon] (
    [Id] int NOT NULL IDENTITY,
    [Codigo] nvarchar(max) NOT NULL,
    [Descripcion] nvarchar(max) NOT NULL,
    [Estado] bit NOT NULL,
    [UsuarioId] int NOT NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [FechaModificacion] datetime2 NOT NULL,
    CONSTRAINT [PK_TipoPagoAdmon] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [TipoTarifa] (
    [Id] int NOT NULL IDENTITY,
    [Codigo] nvarchar(max) NULL,
    [Descripcion] nvarchar(max) NOT NULL,
    [Estado] bit NOT NULL,
    [UsuarioId] int NOT NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [FechaModificacion] datetime2 NOT NULL,
    CONSTRAINT [PK_TipoTarifa] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [TipoVinculacionContractual] (
    [Id] int NOT NULL IDENTITY,
    [Codigo] nvarchar(max) NULL,
    [Descripcion] nvarchar(max) NOT NULL,
    [Estado] bit NOT NULL,
    [UsuarioId] int NOT NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [FechaModificacion] datetime2 NOT NULL,
    CONSTRAINT [PK_TipoVinculacionContractual] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Libros] (
    [Id] int NOT NULL IDENTITY,
    [Titulo] nvarchar(max) NULL,
    [AutorId] int NOT NULL,
    CONSTRAINT [PK_Libros] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Libros_Autores_AutorId] FOREIGN KEY ([AutorId]) REFERENCES [Autores] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Municipio] (
    [Id] int NOT NULL IDENTITY,
    [CodMun] int NOT NULL,
    [Nombre] nvarchar(max) NULL,
    [CodDep] int NOT NULL,
    [DepartamentoId] int NULL,
    [UsuarioId] int NOT NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [FechaModificacion] datetime2 NOT NULL,
    CONSTRAINT [PK_Municipio] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Municipio_Departamentos_DepartamentoId] FOREIGN KEY ([DepartamentoId]) REFERENCES [Departamentos] ([Id])
);
GO

CREATE TABLE [MMenuRoles] (
    [id] int NOT NULL IDENTITY,
    [Rol] NVARCHAR(256) NOT NULL,
    [MMenuId] int NOT NULL,
    [Activo] bit NOT NULL,
    [UserName] nvarchar(max) NULL,
    [UsuarioId] int NOT NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [FechaModificacion] datetime2 NOT NULL,
    CONSTRAINT [PK_MMenuRoles] PRIMARY KEY ([id]),
    CONSTRAINT [FK_MMenuRoles_MMenus_MMenuId] FOREIGN KEY ([MMenuId]) REFERENCES [MMenus] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [TipoConceptoFacturacion] (
    [Id] int NOT NULL IDENTITY,
    [Codigo] nvarchar(max) NOT NULL,
    [Descripcion] nvarchar(max) NOT NULL,
    [Estado] bit NOT NULL,
    [UsuarioId] int NOT NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [FechaModificacion] datetime2 NOT NULL,
    [TipoPagoAdmonId] int NOT NULL,
    CONSTRAINT [PK_TipoConceptoFacturacion] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TipoConceptoFacturacion_TipoPagoAdmon_TipoPagoAdmonId] FOREIGN KEY ([TipoPagoAdmonId]) REFERENCES [TipoPagoAdmon] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [TipoObligacion] (
    [Id] int NOT NULL IDENTITY,
    [Codigo] nvarchar(max) NOT NULL,
    [Descripcion] nvarchar(max) NOT NULL,
    [Estado] bit NOT NULL,
    [UsuarioId] int NOT NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [FechaModificacion] datetime2 NOT NULL,
    [TipoPagoAdmonId] int NOT NULL,
    CONSTRAINT [PK_TipoObligacion] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TipoObligacion_TipoPagoAdmon_TipoPagoAdmonId] FOREIGN KEY ([TipoPagoAdmonId]) REFERENCES [TipoPagoAdmon] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Entidades] (
    [Id] int NOT NULL IDENTITY,
    [Identificacion] nvarchar(max) NOT NULL,
    [Nombre] nvarchar(max) NOT NULL,
    [Direccion] nvarchar(max) NULL,
    [Telefono] nvarchar(max) NULL,
    [Nivel] nvarchar(max) NULL,
    [TipoEmpresaId] int NOT NULL,
    [TipoEmpresaSectorId] int NOT NULL,
    [TipoEmpresaNivelId] int NOT NULL,
    [DepartamentoId] int NOT NULL,
    [MunicipioId] int NOT NULL,
    [Estado] bit NOT NULL,
    [UsuarioId] int NOT NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [FechaModificacion] datetime2 NOT NULL,
    CONSTRAINT [PK_Entidades] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Entidades_Departamentos_DepartamentoId] FOREIGN KEY ([DepartamentoId]) REFERENCES [Departamentos] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Entidades_Municipio_MunicipioId] FOREIGN KEY ([MunicipioId]) REFERENCES [Municipio] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Entidades_TipoEmpresaNivel_TipoEmpresaNivelId] FOREIGN KEY ([TipoEmpresaNivelId]) REFERENCES [TipoEmpresaNivel] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Entidades_TipoEmpresaSector_TipoEmpresaSectorId] FOREIGN KEY ([TipoEmpresaSectorId]) REFERENCES [TipoEmpresaSector] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Entidades_TipoEmpresa_TipoEmpresaId] FOREIGN KEY ([TipoEmpresaId]) REFERENCES [TipoEmpresa] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Sedes] (
    [Id] int NOT NULL IDENTITY,
    [IdentificacionHominis] nvarchar(max) NOT NULL,
    [Nombre] nvarchar(max) NOT NULL,
    [Direccion] nvarchar(max) NULL,
    [Telefono] nvarchar(max) NULL,
    [CedulaCatastral] nvarchar(max) NULL,
    [MatriculaInnoviliaria] nvarchar(max) NULL,
    [TipoVinculacionContractualId] int NOT NULL,
    [TipoInmuebleId] int NOT NULL,
    [DepartamentoId] int NOT NULL,
    [MunicipioId] int NOT NULL,
    [Estado] bit NOT NULL,
    [UsuarioId] int NOT NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [FechaModificacion] datetime2 NOT NULL,
    CONSTRAINT [PK_Sedes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Sedes_Departamentos_DepartamentoId] FOREIGN KEY ([DepartamentoId]) REFERENCES [Departamentos] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Sedes_Municipio_MunicipioId] FOREIGN KEY ([MunicipioId]) REFERENCES [Municipio] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Sedes_TipoInmueble_TipoInmuebleId] FOREIGN KEY ([TipoInmuebleId]) REFERENCES [TipoInmueble] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Sedes_TipoVinculacionContractual_TipoVinculacionContractualId] FOREIGN KEY ([TipoVinculacionContractualId]) REFERENCES [TipoVinculacionContractual] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [EntidadTipoObligaciones] (
    [Id] int NOT NULL IDENTITY,
    [TerceroIdentificacion] nvarchar(max) NULL,
    [Estado] bit NOT NULL,
    [EntidadId] int NOT NULL,
    [TipoObligacionId] int NOT NULL,
    [TipoTarifaId] int NOT NULL,
    [UsuarioId] int NOT NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [FechaModificacion] datetime2 NOT NULL,
    CONSTRAINT [PK_EntidadTipoObligaciones] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_EntidadTipoObligaciones_Entidades_EntidadId] FOREIGN KEY ([EntidadId]) REFERENCES [Entidades] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_EntidadTipoObligaciones_TipoObligacion_TipoObligacionId] FOREIGN KEY ([TipoObligacionId]) REFERENCES [TipoObligacion] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_EntidadTipoObligaciones_TipoTarifa_TipoTarifaId] FOREIGN KEY ([TipoTarifaId]) REFERENCES [TipoTarifa] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [CoordinacionPGNSedes] (
    [Id] int NOT NULL IDENTITY,
    [CoordinacionPGNId] int NOT NULL,
    [SedeId] int NOT NULL,
    [UsuarioId] int NOT NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [FechaModificacion] datetime2 NOT NULL,
    CONSTRAINT [PK_CoordinacionPGNSedes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CoordinacionPGNSedes_CoordinacionPGNs_CoordinacionPGNId] FOREIGN KEY ([CoordinacionPGNId]) REFERENCES [CoordinacionPGNs] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CoordinacionPGNSedes_Sedes_SedeId] FOREIGN KEY ([SedeId]) REFERENCES [Sedes] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [SedeContratos] (
    [Id] int NOT NULL IDENTITY,
    [DocumentoNumero] nvarchar(max) NOT NULL,
    [FechaInicio] datetime2 NULL,
    [FechaFinal] datetime2 NULL,
    [TerceroIdentificacion] nvarchar(max) NULL,
    [TerceroNombres] nvarchar(max) NULL,
    [Notas] nvarchar(max) NULL,
    [Estado] bit NOT NULL,
    [SedeId] int NOT NULL,
    [UsuarioId] int NOT NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [FechaModificacion] datetime2 NOT NULL,
    CONSTRAINT [PK_SedeContratos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SedeContratos_Sedes_SedeId] FOREIGN KEY ([SedeId]) REFERENCES [Sedes] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [SedeEntidades] (
    [Id] int NOT NULL IDENTITY,
    [Notas] nvarchar(max) NULL,
    [SedeId] int NOT NULL,
    [EntidadId] int NOT NULL,
    [UsuarioId] int NOT NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [FechaModificacion] datetime2 NOT NULL,
    CONSTRAINT [PK_SedeEntidades] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SedeEntidades_Entidades_EntidadId] FOREIGN KEY ([EntidadId]) REFERENCES [Entidades] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_SedeEntidades_Sedes_SedeId] FOREIGN KEY ([SedeId]) REFERENCES [Sedes] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_CoordinacionPGNSedes_CoordinacionPGNId] ON [CoordinacionPGNSedes] ([CoordinacionPGNId]);
GO

CREATE INDEX [IX_CoordinacionPGNSedes_SedeId] ON [CoordinacionPGNSedes] ([SedeId]);
GO

CREATE INDEX [IX_Entidades_DepartamentoId] ON [Entidades] ([DepartamentoId]);
GO

CREATE INDEX [IX_Entidades_MunicipioId] ON [Entidades] ([MunicipioId]);
GO

CREATE INDEX [IX_Entidades_TipoEmpresaId] ON [Entidades] ([TipoEmpresaId]);
GO

CREATE INDEX [IX_Entidades_TipoEmpresaNivelId] ON [Entidades] ([TipoEmpresaNivelId]);
GO

CREATE INDEX [IX_Entidades_TipoEmpresaSectorId] ON [Entidades] ([TipoEmpresaSectorId]);
GO

CREATE INDEX [IX_EntidadTipoObligaciones_EntidadId] ON [EntidadTipoObligaciones] ([EntidadId]);
GO

CREATE INDEX [IX_EntidadTipoObligaciones_TipoObligacionId] ON [EntidadTipoObligaciones] ([TipoObligacionId]);
GO

CREATE INDEX [IX_EntidadTipoObligaciones_TipoTarifaId] ON [EntidadTipoObligaciones] ([TipoTarifaId]);
GO

CREATE INDEX [IX_Libros_AutorId] ON [Libros] ([AutorId]);
GO

CREATE INDEX [IX_MMenuRoles_MMenuId] ON [MMenuRoles] ([MMenuId]);
GO

CREATE INDEX [IX_MMenus_MMenuPadreId] ON [MMenus] ([MMenuPadreId]);
GO

CREATE INDEX [IX_Municipio_DepartamentoId] ON [Municipio] ([DepartamentoId]);
GO

CREATE INDEX [IX_SedeContratos_SedeId] ON [SedeContratos] ([SedeId]);
GO

CREATE INDEX [IX_SedeEntidades_EntidadId] ON [SedeEntidades] ([EntidadId]);
GO

CREATE INDEX [IX_SedeEntidades_SedeId] ON [SedeEntidades] ([SedeId]);
GO

CREATE INDEX [IX_Sedes_DepartamentoId] ON [Sedes] ([DepartamentoId]);
GO

CREATE INDEX [IX_Sedes_MunicipioId] ON [Sedes] ([MunicipioId]);
GO

CREATE INDEX [IX_Sedes_TipoInmuebleId] ON [Sedes] ([TipoInmuebleId]);
GO

CREATE INDEX [IX_Sedes_TipoVinculacionContractualId] ON [Sedes] ([TipoVinculacionContractualId]);
GO

CREATE INDEX [IX_TipoConceptoFacturacion_TipoPagoAdmonId] ON [TipoConceptoFacturacion] ([TipoPagoAdmonId]);
GO

CREATE INDEX [IX_TipoObligacion_TipoPagoAdmonId] ON [TipoObligacion] ([TipoPagoAdmonId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230917001419_Inicial', N'7.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Municipio] DROP CONSTRAINT [FK_Municipio_Departamentos_DepartamentoId];
GO

DROP INDEX [IX_Municipio_DepartamentoId] ON [Municipio];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Municipio]') AND [c].[name] = N'DepartamentoId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Municipio] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Municipio] DROP COLUMN [DepartamentoId];
GO

ALTER TABLE [SedeContratos] ADD [TerceroApellidos] nvarchar(max) NULL;
GO

CREATE INDEX [IX_Municipio_CodDep] ON [Municipio] ([CodDep]);
GO

ALTER TABLE [Municipio] ADD CONSTRAINT [FK_Municipio_Departamentos_CodDep] FOREIGN KEY ([CodDep]) REFERENCES [Departamentos] ([Id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230920024306_TerceroActualizar', N'7.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [SedeContratos] ADD [LinkSecop] nvarchar(max) NULL;
GO

ALTER TABLE [SedeContratos] ADD [Meses] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [SedeContratos] ADD [Valor] decimal(18,2) NOT NULL DEFAULT 0.0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230920030639_TerceroActualizar02', N'7.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

EXEC sp_rename N'[EntidadTipoObligaciones].[TerceroIdentificacion]', N'NumeroPagoElectronico', N'COLUMN';
GO

ALTER TABLE [EntidadTipoObligaciones] ADD [NumeroContrato] nvarchar(max) NULL;
GO

ALTER TABLE [EntidadTipoObligaciones] ADD [periodicidadFactura] int NOT NULL DEFAULT 0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230925031506_ModiciacionEmpresaTipoOblicaciones', N'7.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SedeContratos]') AND [c].[name] = N'Meses');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [SedeContratos] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [SedeContratos] DROP COLUMN [Meses];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SedeContratos]') AND [c].[name] = N'TerceroApellidos');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [SedeContratos] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [SedeContratos] DROP COLUMN [TerceroApellidos];
GO

EXEC sp_rename N'[SedeContratos].[TerceroNombres]', N'RazonSocial', N'COLUMN';
GO

EXEC sp_rename N'[SedeContratos].[TerceroIdentificacion]', N'Identificacion', N'COLUMN';
GO

EXEC sp_rename N'[EntidadTipoObligaciones].[periodicidadFactura]', N'PeriodicidadFactura', N'COLUMN';
GO

CREATE TABLE [FacturaEstado] (
    [Id] int NOT NULL IDENTITY,
    [Codigo] nvarchar(max) NULL,
    [Descripcion] nvarchar(max) NOT NULL,
    [ColorLetra] nvarchar(max) NULL,
    [ColorFondo] nvarchar(max) NULL,
    [Estado] bit NOT NULL,
    [UsuarioId] int NOT NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [FechaModificacion] datetime2 NOT NULL,
    CONSTRAINT [PK_FacturaEstado] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [FacturaRegistro] (
    [Id] int NOT NULL IDENTITY,
    [SedeId] int NOT NULL,
    [EntidadId] int NOT NULL,
    [NumeroContrato] nvarchar(max) NULL,
    [FacturaNumero] nvarchar(max) NULL,
    [ReferenciaPago] nvarchar(max) NULL,
    [FechaEmision] datetime2 NULL,
    [FechaPago] datetime2 NULL,
    [ValorFactura] decimal(18,2) NOT NULL,
    [FacturaEstadoId] int NOT NULL,
    [Nota] nvarchar(max) NULL,
    [FechaUltimoPago] datetime2 NULL,
    [FechaProximaFecha] datetime2 NULL,
    [ValorFacturaUltimoPago] decimal(18,2) NOT NULL,
    [UrlFactura] nvarchar(max) NULL,
    [UsuarioId] int NOT NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [FechaModificacion] datetime2 NOT NULL,
    CONSTRAINT [PK_FacturaRegistro] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_FacturaRegistro_Entidades_EntidadId] FOREIGN KEY ([EntidadId]) REFERENCES [Entidades] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_FacturaRegistro_FacturaEstado_FacturaEstadoId] FOREIGN KEY ([FacturaEstadoId]) REFERENCES [FacturaEstado] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_FacturaRegistro_Sedes_SedeId] FOREIGN KEY ([SedeId]) REFERENCES [Sedes] ([Id])
);
GO

CREATE TABLE [FacturaTipoObligacion] (
    [Id] int NOT NULL IDENTITY,
    [FacturaRegistroId] int NOT NULL,
    [TipoObligacionId] int NOT NULL,
    [UsuarioId] int NOT NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [FechaModificacion] datetime2 NOT NULL,
    CONSTRAINT [PK_FacturaTipoObligacion] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_FacturaTipoObligacion_FacturaRegistro_FacturaRegistroId] FOREIGN KEY ([FacturaRegistroId]) REFERENCES [FacturaRegistro] ([Id]),
    CONSTRAINT [FK_FacturaTipoObligacion_TipoObligacion_TipoObligacionId] FOREIGN KEY ([TipoObligacionId]) REFERENCES [TipoObligacion] ([Id])
);
GO

CREATE TABLE [FacturaTipoObligacionConceptos] (
    [Id] int NOT NULL IDENTITY,
    [FacturaTipoObligacionId] int NOT NULL,
    [TipoConceptoFacturacionId] int NOT NULL,
    [Valor] decimal(18,2) NOT NULL,
    [Nota] nvarchar(max) NULL,
    [UsuarioId] int NOT NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [FechaModificacion] datetime2 NOT NULL,
    CONSTRAINT [PK_FacturaTipoObligacionConceptos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_FacturaTipoObligacionConceptos_FacturaTipoObligacion_FacturaTipoObligacionId] FOREIGN KEY ([FacturaTipoObligacionId]) REFERENCES [FacturaTipoObligacion] ([Id]),
    CONSTRAINT [FK_FacturaTipoObligacionConceptos_TipoConceptoFacturacion_TipoConceptoFacturacionId] FOREIGN KEY ([TipoConceptoFacturacionId]) REFERENCES [TipoConceptoFacturacion] ([Id])
);
GO

CREATE INDEX [IX_FacturaRegistro_EntidadId] ON [FacturaRegistro] ([EntidadId]);
GO

CREATE INDEX [IX_FacturaRegistro_FacturaEstadoId] ON [FacturaRegistro] ([FacturaEstadoId]);
GO

CREATE INDEX [IX_FacturaRegistro_SedeId] ON [FacturaRegistro] ([SedeId]);
GO

CREATE INDEX [IX_FacturaTipoObligacion_FacturaRegistroId] ON [FacturaTipoObligacion] ([FacturaRegistroId]);
GO

CREATE INDEX [IX_FacturaTipoObligacion_TipoObligacionId] ON [FacturaTipoObligacion] ([TipoObligacionId]);
GO

CREATE INDEX [IX_FacturaTipoObligacionConceptos_FacturaTipoObligacionId] ON [FacturaTipoObligacionConceptos] ([FacturaTipoObligacionId]);
GO

CREATE INDEX [IX_FacturaTipoObligacionConceptos_TipoConceptoFacturacionId] ON [FacturaTipoObligacionConceptos] ([TipoConceptoFacturacionId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231009213628_Facturas', N'7.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [FacturaRegistro] ADD [Estado] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231012143857_ModificaFactura', N'7.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231012173332_ModificaFacturaTipoObligacion', N'7.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [TipoObligacion] ADD [imagen] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231015175547_ModificaFacturaTipoObligacionImagen', N'7.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [SedeContratos] ADD [email] nvarchar(max) NULL;
GO

ALTER TABLE [SedeContratos] ADD [telefono] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231112183747_ModificarSedeContrato', N'7.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [CoordinacionPGNs] ADD [Estado] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231114195441_CoordinacionAdd-Estado', N'7.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Entidades] ADD [PeriodicidadFactura] int NOT NULL DEFAULT 0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231124124123_ActualizaEntidadPeriodicidad', N'7.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[EntidadTipoObligaciones]') AND [c].[name] = N'NumeroContrato');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [EntidadTipoObligaciones] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [EntidadTipoObligaciones] DROP COLUMN [NumeroContrato];
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[EntidadTipoObligaciones]') AND [c].[name] = N'NumeroPagoElectronico');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [EntidadTipoObligaciones] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [EntidadTipoObligaciones] DROP COLUMN [NumeroPagoElectronico];
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[EntidadTipoObligaciones]') AND [c].[name] = N'PeriodicidadFactura');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [EntidadTipoObligaciones] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [EntidadTipoObligaciones] DROP COLUMN [PeriodicidadFactura];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231124141508_ActualizaEntidadTipoObligacion', N'7.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [SedeEntidades] ADD [NumeroContador] nvarchar(max) NULL;
GO

ALTER TABLE [SedeEntidades] ADD [NumeroContrato] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231129214054_ActualizarSedeEntidad', N'7.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [CoordinacionPGNs] ADD [JefeCoordinadorEmail] nvarchar(max) NULL;
GO

ALTER TABLE [CoordinacionPGNs] ADD [JefeCoordinadorNombre] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231201152926_ActualizarCoordinador', N'7.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

EXEC sp_rename N'[FacturaRegistro].[FechaUltimoPago]', N'FechaPeriodoFacturaInicio', N'COLUMN';
GO

EXEC sp_rename N'[FacturaRegistro].[FechaProximaFecha]', N'FechaPeriodoFacturaFin', N'COLUMN';
GO

EXEC sp_rename N'[FacturaRegistro].[FechaPago]', N'FechaOportunoPago', N'COLUMN';
GO

ALTER TABLE [FacturaRegistro] ADD [PagoInmediato] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231201223423_ActualizarFacturaRegistro', N'7.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [FacturaRegistro] ADD [FechFacturaUltimoPagon] datetime2 NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231228200024_facturaregistroAddFechaUltimoPago', N'7.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

EXEC sp_rename N'[FacturaRegistro].[FechFacturaUltimoPagon]', N'FechFacturaUltimoPago', N'COLUMN';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231229164618_facturaregistroAddFechaUltimoPago-2', N'7.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

EXEC sp_rename N'[FacturaRegistro].[FechFacturaUltimoPago]', N'FechaFacturaUltimoPago', N'COLUMN';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231229172921_facturaregistroAddFechaUltimoPago-3', N'7.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [FacturaTipoObligacion] ADD [ConsumoMes] decimal(18,2) NOT NULL DEFAULT 0.0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231230180305_facturatipoobligacionAddConsumomes', N'7.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[FacturaRegistro]') AND [c].[name] = N'UrlFactura');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [FacturaRegistro] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [FacturaRegistro] DROP COLUMN [UrlFactura];
GO

ALTER TABLE [FacturaRegistro] ADD [ValorFacturaxConcepto] decimal(18,2) NOT NULL DEFAULT 0.0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231230184024_facturaUpdate', N'7.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231230234535_AddDocumento-FacturaDocumento', N'7.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [TipoDocumentos] (
    [Id] int NOT NULL IDENTITY,
    [Descripcion] nvarchar(max) NULL,
    [Estado] bit NOT NULL,
    [UsuarioId] int NOT NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [FechaModificacion] datetime2 NOT NULL,
    CONSTRAINT [PK_TipoDocumentos] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [FacturaDocumentos] (
    [Id] int NOT NULL IDENTITY,
    [FacturaRegistroId] int NOT NULL,
    [TipoDocumentosId] int NOT NULL,
    [Nota] nvarchar(max) NULL,
    [url] nvarchar(max) NULL,
    [Estado] bit NOT NULL,
    [NombreArchivo] nvarchar(max) NULL,
    [UsuarioId] int NOT NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [FechaModificacion] datetime2 NOT NULL,
    CONSTRAINT [PK_FacturaDocumentos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_FacturaDocumentos_FacturaRegistro_FacturaRegistroId] FOREIGN KEY ([FacturaRegistroId]) REFERENCES [FacturaRegistro] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_FacturaDocumentos_TipoDocumentos_TipoDocumentosId] FOREIGN KEY ([TipoDocumentosId]) REFERENCES [TipoDocumentos] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_FacturaDocumentos_FacturaRegistroId] ON [FacturaDocumentos] ([FacturaRegistroId]);
GO

CREATE INDEX [IX_FacturaDocumentos_TipoDocumentosId] ON [FacturaDocumentos] ([TipoDocumentosId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231230234739_AddDocumento-FacturaDocumento-1', N'7.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [TipoDocumentos] ADD [Codigo] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231231001836_AddDocumento-FacturaDocumento-2', N'7.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240105195311_SistemaUsuarios', N'7.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CoordinacionPGNs]') AND [c].[name] = N'UsuarioId');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [CoordinacionPGNs] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [CoordinacionPGNs] DROP COLUMN [UsuarioId];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240107135942_CoordinadorEliminarUsuarioId', N'7.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [CoordinacionPGNs] ADD [UsuarioId] nvarchar(450) NULL;
GO

CREATE INDEX [IX_CoordinacionPGNs_UsuarioId] ON [CoordinacionPGNs] ([UsuarioId]);
GO

ALTER TABLE [CoordinacionPGNs] ADD CONSTRAINT [FK_CoordinacionPGNs_AspNetUsers_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [AspNetUsers] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240107140102_CoordinadorAddUsuarioId', N'7.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
    SET IDENTITY_INSERT [AspNetRoles] ON;
INSERT INTO [AspNetRoles] ([Id], [ConcurrencyStamp], [Name], [NormalizedName])
VALUES (N'10c405b5-2859-4312-b74f-577736bfc0c3', N'4ed99591-3ae0-4d24-986e-7b178734fe97', N'Admin', N'Admin');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
    SET IDENTITY_INSERT [AspNetRoles] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'ConcurrencyStamp', N'Email', N'EmailConfirmed', N'LockoutEnabled', N'LockoutEnd', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[AspNetUsers]'))
    SET IDENTITY_INSERT [AspNetUsers] ON;
INSERT INTO [AspNetUsers] ([Id], [AccessFailedCount], [ConcurrencyStamp], [Email], [EmailConfirmed], [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TwoFactorEnabled], [UserName])
VALUES (N'9b9524b0-a0ec-4de3-ad79-cfb125d40c4f', 0, N'be471309-c0cb-4b8e-aeab-e62b99f388fb', N'omurgas@hotmail.com', CAST(0 AS bit), CAST(0 AS bit), NULL, N'omurgas@hotmail.com', N'omurgas@hotmail.com', N'AQAAAAEAACcQAAAAEPOQxENDHIVJ2gSfahYVlor2V96YFe98wif1lFTQdlQLz7muTxYlb7W9e6JDaW95fg==', NULL, CAST(0 AS bit), N'e524f8f4-92b4-4882-9007-be627ffc13ba', CAST(0 AS bit), N'omurgas@hotmail.com');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'ConcurrencyStamp', N'Email', N'EmailConfirmed', N'LockoutEnabled', N'LockoutEnd', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[AspNetUsers]'))
    SET IDENTITY_INSERT [AspNetUsers] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ClaimType', N'ClaimValue', N'UserId') AND [object_id] = OBJECT_ID(N'[AspNetUserClaims]'))
    SET IDENTITY_INSERT [AspNetUserClaims] ON;
INSERT INTO [AspNetUserClaims] ([Id], [ClaimType], [ClaimValue], [UserId])
VALUES (1, N'http://schemas.microsoft.com/ws/2008/06/identity/claims/role', N'Admin', N'9b9524b0-a0ec-4de3-ad79-cfb125d40c4f');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ClaimType', N'ClaimValue', N'UserId') AND [object_id] = OBJECT_ID(N'[AspNetUserClaims]'))
    SET IDENTITY_INSERT [AspNetUserClaims] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleId', N'UserId') AND [object_id] = OBJECT_ID(N'[AspNetUserRoles]'))
    SET IDENTITY_INSERT [AspNetUserRoles] ON;
INSERT INTO [AspNetUserRoles] ([RoleId], [UserId])
VALUES (N'10c405b5-2859-4312-b74f-577736bfc0c3', N'9b9524b0-a0ec-4de3-ad79-cfb125d40c4f');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleId', N'UserId') AND [object_id] = OBJECT_ID(N'[AspNetUserRoles]'))
    SET IDENTITY_INSERT [AspNetUserRoles] OFF;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240109180332_AdminData', N'7.0.4');
GO

COMMIT;
GO

