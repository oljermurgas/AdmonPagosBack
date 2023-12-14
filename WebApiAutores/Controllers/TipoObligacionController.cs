using AdminPagosApi.DTO;
using AdminPagosApi.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using WebApiAutores;
using WebApiAutores.Entidades;

namespace AdminPagosApi.Controllers
{
    [ApiController]
    [Route("AdmonPago/Api/TipoObligacion")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TipoObligacionController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public TipoObligacionController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<TipoObligacionDTO>>> Get()
        {
            var entidades = await context.TipoObligacion
                                 .Include(x => x.TipoPagoAdmon)
                                 .OrderBy(e => e.Codigo)
                                 .ToListAsync();
            var dtos = mapper.Map<List<TipoObligacionDTO>>(entidades);
            return dtos;
        }

        [HttpGet("entidad/{id:int}")]
        public async Task<ActionResult<List<EntidadTipoObligacionDTO>>> ObtnerPorEntidadId(int id)
        {
            var entidades = await context.EntidadTipoObligaciones
                .Include(e => e.TipoObligaciones) // Incluye las TipoObligaciones
                .Include(x => x.TipoObligaciones.TipoPagoAdmon)
                .Where(e => e.Entidades.Id == id) // Filtra por el ID de la entidad
                .ToListAsync();

            if (entidades == null || entidades.Count == 0)
            {
                return NotFound(); // Devuelve un 404 si no se encuentran entidades
            }

            // Mapea las entidades a EntidadTipoObligacionDTO
            var dtos = mapper.Map<List<EntidadTipoObligacionDTO>>(entidades);

            return Ok(dtos);
        }

        [HttpGet("entidad/activas/{id:int}")]
        public async Task<ActionResult<List<EntidadTipoObligacionDTO>>> ObtnerPorEntidadActivasId(int id)
        {
            var tipoObligaciones = await context.EntidadTipoObligaciones
                 .Where(eto => eto.EntidadId == id && eto.Estado == true)
                 .Include(eto => eto.TipoObligaciones.TipoPagoAdmon)
                 .Select(eto => new TipoObligacionDTO
                 {
                     Id = eto.TipoObligaciones.Id,
                     Codigo = eto.TipoObligaciones.Codigo,
                     Descripcion = eto.TipoObligaciones.Descripcion,
                     TipoPagoAdmonId = eto.TipoObligaciones.TipoPagoAdmonId,
                     Estado = eto.Estado
                 })
                 .ToListAsync();

            if (tipoObligaciones == null || tipoObligaciones.Count == 0)
            {
                return NotFound();
            }

            return Ok(tipoObligaciones);
        }


        [HttpGet("{codigo}")]
        public async Task<ActionResult<TipoObligacionDTO>> Get(string codigo)
        {
            var entidades = await context.TipoObligacion.FirstOrDefaultAsync(x => x.Codigo.Contains(codigo));
            if (entidades == null)
            {
                return NotFound();
            }
            var dtos = mapper.Map<TipoObligacionDTO>(entidades);
            return dtos;
        }

        [HttpGet("porid/{id:int}", Name = "obtenertipoobligacionporid")]
        public async Task<ActionResult<List<TipoObligacionDTO>>> ObtenerPorId(int id)
        {
            var entidad = await context.TipoObligacion
                                        .Where(x => x.TipoPagoAdmonId == id)
                                        .OrderBy(x => x.Descripcion)
                                        .ToListAsync();
            if (entidad == null) {
                return NotFound();
            }

            var dto = mapper.Map<List<TipoObligacionDTO>>(entidad);
            return dto;
        }



        [HttpGet("{id:int}", Name = "obtenertipoobligacion")]
        public async Task<ActionResult<TipoObligacionDTO>> Get(int id)
        {
            var entidad = await context.TipoObligacion.FirstOrDefaultAsync(x => x.Id == id);
            if (entidad == null)
            {
                return NotFound();
            }
            var dtos = mapper.Map<TipoObligacionDTO>(entidad);
            return dtos;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TipoObligacionDTOCR tipoObligacionDTOCR)
        {
            try
            {
                var ValideExistencia = await ValidarExistencia(tipoObligacionDTOCR);
                if (ValideExistencia)
                {
                    return BadRequest(new { message = "El tipo obligación: >> " + tipoObligacionDTOCR.Codigo + " << ya existe" });
                }

                //var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
                var entidad = mapper.Map<TipoObligacion>(tipoObligacionDTOCR);
                //-----------------------------------------------------------------------------------------
                //entidad.UserName = userName;
                entidad.FechaCreacion = DateTime.Now;
                entidad.Estado = true;
                //-----------------------------------------------------------------------------------------
                context.Add(entidad);
                await context.SaveChangesAsync();
                var entidadDTO = mapper.Map<TipoObligacionDTO>(entidad);
                return new CreatedAtRouteResult("obtenertipoobligacion", new { id = entidadDTO.Id }, entidadDTO);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] TipoPagoAdmonDTO tipoPagoAdmonDTO)
        {
            var existe = await context.TipoObligacion.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            //var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            var entidad = mapper.Map<TipoTarifa>(tipoPagoAdmonDTO);
            //-----------------------------------------------------------------------------------------
            //entidad.UserName = userName;
            entidad.FechaModificacion = DateTime.Now;
            //-----------------------------------------------------------------------------------------
            entidad.Id = id;
            context.Entry(entidad).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch(int id, JsonPatchDocument<TipoObligacionDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }
            var tipoEmpresaDB = await context.TipoObligacion.FirstOrDefaultAsync(x => x.Id == id);

            if (tipoEmpresaDB == null)
            {
                return NotFound();
            }

            var tipoEmpresaDTO = mapper.Map<TipoObligacionDTO>(tipoEmpresaDB);
            patchDocument.ApplyTo(tipoEmpresaDTO, ModelState);

            var esValido = TryValidateModel(tipoEmpresaDTO);
            if (!esValido)
            {
                return BadRequest(ModelState);
            }

            mapper.Map(tipoEmpresaDTO, tipoEmpresaDB);
            tipoEmpresaDB.FechaModificacion = DateTime.Now;

            await context.SaveChangesAsync();
            return NoContent();

        }



        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.TipoObligacion.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            var entidad = await context.TipoObligacion.FirstOrDefaultAsync(x => x.Id == id);
            entidad.Id = id;
            entidad.Estado = false;
            context.Entry(entidad).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> ValidarExistencia(TipoObligacionDTOCR tipoObligacionDTOCR)
        {
            var ValidarExistencia = await context.TipoObligacion.AnyAsync(x => (x.Codigo == tipoObligacionDTOCR.Codigo));
            return ValidarExistencia;
        }

    }
}
