using AdminPagosApi.Entidades;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.Entidades;

namespace AdminPagosApi
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Autor> Autores { get; set; }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<CoordinacionPGN> CoordinacionPGNs { get; set; }
        public DbSet<CoordinacionPGNSede> CoordinacionPGNSedes { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Municipio> Municipio { get; set; }
        public DbSet<Entidad> Entidades { get; set; }
        public DbSet<EntidadTipoObligacion> EntidadTipoObligaciones { get; set; }
        public DbSet<MMenu> MMenus { get; set; }
        public DbSet<MMenuRol> MMenuRoles { get; set; }
        public DbSet<Sede> Sedes { get; set; }
        public DbSet<SedeContrato> SedeContratos { get; set; }
        public DbSet<SedeEntidad> SedeEntidades { get; set; }
        public DbSet<TipoCanalEnvioFactura> TipoCanalEnvioFactura { get; set; }
        public DbSet<TipoConceptoFacturacion> TipoConceptoFacturacion { get; set; }
        public DbSet<TipoEmpresa> TipoEmpresa { get; set; }
        public DbSet<TipoEmpresaNivel> TipoEmpresaNivel { get; set; }
        public DbSet<TipoEmpresaSector> TipoEmpresaSector { get; set; }
        public DbSet<TipoInmueble> TipoInmueble { get; set; }
        public DbSet<TipoObligacion> TipoObligacion { get; set; }
        public DbSet<TipoPagoAdmon> TipoPagoAdmon { get; set; }
        public DbSet<TipoTarifa> TipoTarifa { get; set; }
        public DbSet<TipoVinculacionContractual> TipoVinculacionContractual { get; set; }

        public DbSet<FacturaEstado> FacturaEstado { get; set; }
        public DbSet<FacturaRegistro> FacturaRegistro { get; set; }
        public DbSet<FacturaTipoObligacion> FacturaTipoObligacion { get; set; }

        public DbSet<FacturaTipoObligacionConceptos> FacturaTipoObligacionConceptos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Municipio>()
                .HasOne(m => m.Departamento)
                .WithMany(d => d.Municipios)
                .HasForeignKey(m => m.CodDep);

            // Otras configuraciones de tus modelos
        }
    }
}

