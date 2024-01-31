using AdminPagosApi.Entidades;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApiAutores.Entidades;

namespace AdminPagosApi
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly DbContextOptions _options;
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            _options = options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //SeedData(modelBuilder);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Municipio>()
                .HasOne(m => m.Departamento)
                .WithMany(d => d.Municipios)
                .HasForeignKey(m => m.CodDep);

        }


        //private void SeedData(ModelBuilder modelBuilder)
        ////private void SeedData()
        //{
        //    var rolAdminId = "10c405b5-2859-4312-b74f-577736bfc0c3";
        //    var usuarioAdminId = "9b9524b0-a0ec-4de3-ad79-cfb125d40c4f";

        //    var rolAdmin = new IdentityRole()
        //    {
        //        Id = rolAdminId,
        //        Name = "Admin",
        //        NormalizedName = "Admin"
        //    };

        //    var passwordHasher = new PasswordHasher<ApplicationUser>();
        //    var username = "omurgas@hotmail.com";
        //    var usuarioAdmin = new ApplicationUser()
        //    {
        //        Id = usuarioAdminId,
        //        UserName = username,
        //        NormalizedUserName = username,
        //        Email = username,
        //        NormalizedEmail = username,
        //        PasswordHash = passwordHasher.HashPassword(null, "Aa123456!")
        //    };

        //    var userRoles = new IdentityUserRole<string>
        //    {
        //        RoleId = rolAdminId,
        //        UserId = usuarioAdminId
        //    };

        //    modelBuilder.Entity<ApplicationUser>().HasData(usuarioAdmin);
        //    modelBuilder.Entity<IdentityRole>().HasData(rolAdmin);
        //    modelBuilder.Entity<IdentityUserRole<string>>().HasData(userRoles);
        //    modelBuilder.Entity<IdentityUserClaim<string>>().HasData(new IdentityUserClaim<string>()
        //    {
        //        Id = 1,
        //        ClaimType = ClaimTypes.Role,
        //        UserId = usuarioAdminId,
        //        ClaimValue = "Admin"
        //    });
        //}

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
        public DbSet<FacturaDocumentos> FacturaDocumentos { get; set; }
        public DbSet<TipoDocumentos> TipoDocumentos { get; set; }
        public DbSet<Firmas> Firmas { get; set; }   

    }
}

