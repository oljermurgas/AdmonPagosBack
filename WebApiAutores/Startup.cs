using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;


namespace AdminPagosApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration) 
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration;
        
        public void ConfigureServices( IServiceCollection services) 
        {
            services.AddControllers().AddJsonOptions(x =>
                    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles).AddNewtonsoftJson();

            services.AddDbContext<ApplicationDbContext>(options => 
                            options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));

            services.AddAutoMapper(typeof(Startup));
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // Agregar configuración de Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Mi API",
                    Version = "v1"
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
            app.UseRouting();

            app.UseAuthorization();
            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
