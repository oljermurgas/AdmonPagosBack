using AdminPagosApi.DTO;
using AdminPagosApi.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApiAutores;
using WebApiAutores.Entidades;

namespace AdminPagosApi.Controllers
{
    [ApiController]
    [Route("AdmonPago/Api/SedeEntidad")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SedeEntidadController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public SedeEntidadController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<SedeEntidadDTO>>> Get()
        {
            var entidades = await context.SedeEntidades
                                 .OrderBy(e => e.Entidades.Nombre)
                                 .ToListAsync();
            var dtos = mapper.Map<List<SedeEntidadDTO>>(entidades);
            return dtos;
        }


        [HttpGet("list/{id:int}")]
        public async Task<ActionResult<List<SedeEntidadDTO>>> GetList(int id)
        {
            var entidades = await context.SedeEntidades
                .Include(x => x.Entidades)
                .Where(x => x.SedeId == id).ToListAsync();

            if (!entidades.Any())
            {
                return NotFound();
            }

            var dtos = mapper.Map<List<SedeEntidadDTO>>(entidades);
            return Ok(dtos);
        }



        [HttpGet("{id:int}", Name = "obtenersedeentidad")]
        public async Task<ActionResult<List<SedeEntidadDTO>>> Get(int id)
        {
            var entidad = await context.SedeEntidades.FirstOrDefaultAsync(x => x.Id == id);
            if (entidad == null)
            {
                return NotFound();
            }
            var dtos = mapper.Map<List<SedeEntidadDTO>>(entidad);
            return dtos;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] SedeEntidadDTOCR sedeEntidadDTOCR)
        {
            try
            {
                var ValideExistencia = await ValidarExistencia(sedeEntidadDTOCR);
                if (ValideExistencia)
                {
                    return BadRequest(new { message = "La sede : >> " + sedeEntidadDTOCR.EntidadId + " << ya existe" });
                }

                //var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
                var entidad = mapper.Map<SedeEntidad>(sedeEntidadDTOCR);
                //-----------------------------------------------------------------------------------------
                //entidad.UserName = userName;
                entidad.FechaCreacion = DateTime.Now;
        
                //-----------------------------------------------------------------------------------------
                context.Add(entidad);
                await context.SaveChangesAsync();
                var entidadDTO = mapper.Map<SedeEntidadDTO>(entidad);
                return new CreatedAtRouteResult("obtenersedeentidad", new { id = entidadDTO.Id }, entidadDTO);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] SedeEntidadDTO sedeEntidadDTO)
        {
            var existe = await context.SedeEntidades.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            //var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            var entidad = mapper.Map<SedeEntidad>(sedeEntidadDTO);
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
            var entidad = await context.SedeEntidades.FirstOrDefaultAsync(x => x.Id == id);
            if (entidad == null)
            {
                return NotFound();
            }
            context.SedeEntidades.Remove(entidad);
            await context.SaveChangesAsync();
            return NoContent();
        }


        private async Task<bool> ValidarExistencia(SedeEntidadDTOCR sedeEntidadDTOCR)
        {
            var ValidarExistencia = await context.SedeEntidades.AnyAsync(x => (x.EntidadId == sedeEntidadDTOCR.EntidadId && x.SedeId == sedeEntidadDTOCR.SedeId));
            return ValidarExistencia;
        }

    }
}
