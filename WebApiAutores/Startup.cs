using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AdminPagosApi.Entidades;
using System.IdentityModel.Tokens.Jwt;
using AdminPagosApi.Servicios;

namespace AdminPagosApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration) 
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            Configuration = configuration;
        }

        public IConfiguration Configuration;
        
        public void ConfigureServices( IServiceCollection services) 
        {
            services.AddControllers().AddJsonOptions(x =>
                    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles).AddNewtonsoftJson();

            services.AddDbContext<ApplicationDbContext>(options => 
                            options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));

            services.AddTransient<ILlamadoProcedimientoAlmacenado, LLamadoProcedimientoAlmacenado>();
            services.AddTransient<IAlmacenadorArchivos, AlmacenadorArchivoLocal>();
            services.AddHttpContextAccessor();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                   .AddEntityFrameworkStores<ApplicationDbContext>()
                   .AddDefaultTokenProviders();

            services.AddAuthorization(opciones =>
            {
                opciones.AddPolicy("esAdmin", politica => politica.RequireClaim("esAdmin"));
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                                options.TokenValidationParameters = new TokenValidationParameters
                                {
                                    ValidateIssuer = false,
                                    ValidateAudience = false,
                                    ValidateLifetime = true,
                                    ValidateIssuerSigningKey = true,
                                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["jwt:key"])),
                                    ClockSkew = TimeSpan.Zero
                                });


            services.AddAutoMapper(typeof(Startup));
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // Agregar configuración de Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPIAdmonPagos", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                { Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header
                });

                c.AddSecurityRequirement( new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id =  "Bearer"
                            }
                        },
                        new string[]
                        {

                        }
                    }
                });

            });

            services.AddCors(opciones =>
            {  opciones.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();    
                });
            });
        }
        
        public void Configure( IApplicationBuilder app, IWebHostEnvironment env) 
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();   
            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
