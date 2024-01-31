using AdminPagosApi.DTO;
using AdminPagosApi.Entidades;
using AdminPagosApi.Servicios;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApiAutores;
using WebApiAutores.Entidades;

namespace AdminPagosApi.Controllers
{
    [ApiController]
    [Route("AdmonPago/Api/Firmas")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FirmasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string contenedor = "firmas";

        public FirmasController(ApplicationDbContext context, 
                   IMapper mapper,
                   IAlmacenadorArchivos almacenadorArchivos)
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        [HttpGet]
        public async Task<ActionResult<List<FirmasDTO>>> Get()
        {
            var entidades = await context.Firmas
                                // .Include(x => x.TipoPagoAdmon)
                                 .OrderBy(e => e.Descripcion)
                                 .ToListAsync();
            var dtos = mapper.Map<List<FirmasDTO>>(entidades);
            return dtos;
        }


        [HttpGet("tipoPagoAdmon/{id:int}", Name = "obtnerfirmasxtipoPagoAdmonId")]
        public async Task<ActionResult<FirmasDTO>> GetByTipoPagoAdmonId(int id)
        {
            var entidad = await context.Firmas.FirstOrDefaultAsync(x => x.TipoPagoAdmonId == id);
            if (entidad == null)
            {
                return NotFound();
            }
            var dtos = mapper.Map<FirmasDTO>(entidad);
            return dtos;
        }

        [HttpGet("{id:int}", Name = "obtnerfirmas")]
        public async Task<ActionResult<FirmasDTO>> Get(int id)
        {
            var entidad = await context.Firmas.FirstOrDefaultAsync(x => x.Id == id);
            if (entidad == null)
            {
                return NotFound();
            }
            var dtos = mapper.Map<FirmasDTO>(entidad);
            return dtos;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] FirmasDTOCR firmasDTOCR)
        {
            try
            {
                var ValideExistencia = await ValidarExistencia(firmasDTOCR);
                if (ValideExistencia)
                {
                    return BadRequest(new { message = "Las firmas para este tipo de Obligacion ya estan registradas" });
                }

                //var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
                var entidad = mapper.Map<Firmas>(firmasDTOCR);

                if (firmasDTOCR.FuncionarioApruebaImagenFirma != null && firmasDTOCR.FuncionarioElaboroImagenFirma !=null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await firmasDTOCR.FuncionarioApruebaImagenFirma.CopyToAsync(memoryStream);
                        var contenido = memoryStream.ToArray();
                        var extension = Path.GetExtension(firmasDTOCR.FuncionarioApruebaImagenFirma.FileName);
                        entidad.FuncionarioApruebaFirma = await almacenadorArchivos.GuardarArchivo(contenido, extension, contenedor, firmasDTOCR.FuncionarioApruebaImagenFirma.ContentType);

                        memoryStream.Seek(0, SeekOrigin.Begin);

                        await firmasDTOCR.FuncionarioElaboroImagenFirma.CopyToAsync(memoryStream);
                         contenido = memoryStream.ToArray();
                         extension = Path.GetExtension(firmasDTOCR.FuncionarioElaboroImagenFirma.FileName);
                         entidad.FuncionarioElaboroFirma = await almacenadorArchivos.GuardarArchivo(contenido, extension, contenedor, firmasDTOCR.FuncionarioElaboroImagenFirma.ContentType);
                    }
                }else
                {
                    return BadRequest(new { message = "falta registrar las firmas" });
                }


                //-----------------------------------------------------------------------------------------
                //entidad.UserName = userName;
                entidad.FechaCreacion = DateTime.Now;
                entidad.Estado = true;
                //-----------------------------------------------------------------------------------------
                context.Add(entidad);
                await context.SaveChangesAsync();
                var entidadDTO = mapper.Map<FirmasDTO>(entidad);
                return new CreatedAtRouteResult("obtnerfirmas", new { id = entidadDTO.Id }, entidadDTO);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] FirmasDTOCR firmasDTOCR)
        {
            var entidad = await context.Firmas.FirstOrDefaultAsync(x => x.Id == id);
            if (entidad == null ) {return NotFound();}

            //var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            entidad = mapper.Map(firmasDTOCR, entidad); // Esto mapeará solo las propiedades presentes en firmasDTOCR

            if (firmasDTOCR.FuncionarioApruebaImagenFirma != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await firmasDTOCR.FuncionarioApruebaImagenFirma.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(firmasDTOCR.FuncionarioApruebaImagenFirma.FileName);
                    entidad.FuncionarioApruebaFirma = await almacenadorArchivos.EditarArchivo(contenido, extension, contenedor, entidad.FuncionarioApruebaFirma, firmasDTOCR.FuncionarioApruebaImagenFirma.ContentType);
                }
            }

            if (firmasDTOCR.FuncionarioElaboroImagenFirma != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await firmasDTOCR.FuncionarioElaboroImagenFirma.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(firmasDTOCR.FuncionarioElaboroImagenFirma.FileName);
                    entidad.FuncionarioElaboroFirma = await almacenadorArchivos.EditarArchivo(contenido, extension, contenedor, entidad.FuncionarioElaboroFirma, firmasDTOCR.FuncionarioElaboroImagenFirma.ContentType);
                }
            }
            //-----------------------------------------------------------------------------------------
            //entidad.UserName = userName;
            entidad.FechaModificacion = DateTime.Now;
            //-----------------------------------------------------------------------------------------
            await context.SaveChangesAsync();
            var dto = mapper.Map<FirmasDTO>(entidad);
            return new CreatedAtRouteResult("obtnerfirmas", new { id = entidad.Id }, dto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Firmas.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            var entidad = await context.Firmas.FirstOrDefaultAsync(x => x.Id == id);
            entidad.Id = id;
            entidad.Estado = false;
            context.Entry(entidad).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> ValidarExistencia(FirmasDTOCR firmasDTOCR)
        {
            var ValidarExistencia = await context.Firmas.AnyAsync(x => (x.TipoPagoAdmonId == firmasDTOCR.TipoPagoAdmonId));
            return ValidarExistencia;
        }

    }
}
