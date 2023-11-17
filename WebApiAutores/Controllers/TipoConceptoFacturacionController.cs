using AdminPagosApi.DTO;
using AdminPagosApi.Entidades;
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
    [Route("AdmonPago/Api/TipoConceptoFacturacion")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TipoConceptoFacturacionController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public TipoConceptoFacturacionController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<TipoConceptoFacturacionDTO>>> Get()
        {
            var entidades = await context.TipoConceptoFacturacion
                                // .Include(x => x.TipoPagoAdmon)
                                 .OrderBy(e => e.Codigo)
                                 .ToListAsync();
            var dtos = mapper.Map<List<TipoConceptoFacturacionDTO>>(entidades);
            return dtos;
        }

        [HttpGet("{codigo}")]
        public async Task<ActionResult<TipoConceptoFacturacionDTO>> Get(string codigo)
        {
            var entidades = await context.TipoConceptoFacturacion.FirstOrDefaultAsync(x => x.Codigo.Contains(codigo));
            if (entidades == null)
            {
                return NotFound();
            }
            var dtos = mapper.Map<TipoConceptoFacturacionDTO>(entidades);
            return dtos;
        }

        [HttpGet("{id:int}", Name = "obtenertipoconceptofacturacion")]
        public async Task<ActionResult<TipoConceptoFacturacionDTO>> Get(int id)
        {
            var entidad = await context.TipoConceptoFacturacion.FirstOrDefaultAsync(x => x.Id == id);
            if (entidad == null)
            {
                return NotFound();
            }
            var dtos = mapper.Map<TipoConceptoFacturacionDTO>(entidad);
            return dtos;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TipoConceptoFacturacionDTOCR tipoConceptoFacturacionDTOCR)
        {
            try
            {
                var ValideExistencia = await ValidarExistencia(tipoConceptoFacturacionDTOCR);
                if (ValideExistencia)
                {
                    return BadRequest(new { message = "El tipo concepto facturacio: >> " + tipoConceptoFacturacionDTOCR.Codigo + " << ya existe" });
                }

                //var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
                var entidad = mapper.Map<TipoConceptoFacturacion>(tipoConceptoFacturacionDTOCR);
                //-----------------------------------------------------------------------------------------
                //entidad.UserName = userName;
                entidad.FechaCreacion = DateTime.Now;
                entidad.Estado = true;
                //-----------------------------------------------------------------------------------------
                context.Add(entidad);
                await context.SaveChangesAsync();
                var entidadDTO = mapper.Map<TipoConceptoFacturacionDTO>(entidad);
                return new CreatedAtRouteResult("obtenertipoconceptofacturacion", new { id = entidadDTO.Id }, entidadDTO);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] TipoConceptoFacturacionDTO tipoConceptoFacturacionDTO)
        {
            var existe = await context.TipoConceptoFacturacion.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            //var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            var entidad = mapper.Map<TipoConceptoFacturacion>(tipoConceptoFacturacionDTO);
            //-----------------------------------------------------------------------------------------
            //entidad.UserName = userName;
            entidad.FechaModificacion = DateTime.Now;
            //-----------------------------------------------------------------------------------------
            entidad.Id = id;
            context.Entry(entidad).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.TipoConceptoFacturacion.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            var entidad = await context.TipoConceptoFacturacion.FirstOrDefaultAsync(x => x.Id == id);
            entidad.Id = id;
            entidad.Estado = false;
            context.Entry(entidad).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> ValidarExistencia(TipoConceptoFacturacionDTOCR tipoConceptoFacturacionDTOCR)
        {
            var ValidarExistencia = await context.TipoConceptoFacturacion.AnyAsync(x => (x.Codigo == tipoConceptoFacturacionDTOCR.Codigo && x.TipoPagoAdmonId == tipoConceptoFacturacionDTOCR.TipoPagoAdmonId));
            return ValidarExistencia;
        }

    }
}
