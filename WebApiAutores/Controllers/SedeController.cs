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
    [Route("AdmonPago/Api/Sede")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SedeController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public SedeController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<SedeDTO>>> Get()
        {
            var entidades = await context.Sedes
                                 .OrderBy(e => e.Nombre)
                                 .ToListAsync();
            var dtos = mapper.Map<List<SedeDTO>>(entidades);
            return dtos;
        }

        [HttpGet("{codigo}")]
        public async Task<ActionResult<SedeDTO>> Get(string nombre)
        {
            var entidades = await context.Sedes.FirstOrDefaultAsync(x => x.Nombre.Contains(nombre));
            if (entidades == null)
            {
                return NotFound();
            }
            var dtos = mapper.Map<SedeDTO>(entidades);
            return dtos;
        }

        [HttpGet("{id:int}", Name = "obtenersede")]
        public async Task<ActionResult<SedeDTO>> Get(int id)
        {
            var entidad = await context.Sedes.FirstOrDefaultAsync(x => x.Id == id);
            if (entidad == null)
            {
                return NotFound();
            }
            var dtos = mapper.Map<SedeDTO>(entidad);
            return dtos;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] SedeDTOCR sedeDTOCR)
        {
            try
            {
                var ValideExistencia = await ValidarExistencia(sedeDTOCR);
                if (ValideExistencia)
                {
                    return BadRequest(new { message = "La sede : >> " + sedeDTOCR.Nombre + " << ya existe" });
                }

                //var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
                var entidad = mapper.Map<Sede>(sedeDTOCR);
                //-----------------------------------------------------------------------------------------
                //entidad.UserName = userName;
                entidad.FechaCreacion = DateTime.Now;
                entidad.Estado = true;
                //-----------------------------------------------------------------------------------------
                context.Add(entidad);
                await context.SaveChangesAsync();
                var entidadDTO = mapper.Map<SedeDTO>(entidad);
                return new CreatedAtRouteResult("obtenersede", new { id = entidadDTO.Id }, entidadDTO);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] SedeDTO sedeDTO)
        {
            var existe = await context.Sedes.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            //var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            var entidad = mapper.Map<Sede>(sedeDTO);
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
        public async Task<ActionResult> Patch(int id, JsonPatchDocument<SedeDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }
            var sedeDB = await context.Sedes.FirstOrDefaultAsync(x => x.Id == id);

            if (sedeDB == null)
            {
                return NotFound();
            }

            var sedeDTO = mapper.Map<SedeDTO>(sedeDB);
            patchDocument.ApplyTo(sedeDTO, ModelState);

            var esValido = TryValidateModel(sedeDTO);
            if (!esValido)
            {
                return BadRequest(ModelState);
            }

            mapper.Map(sedeDTO, sedeDB);
            sedeDB.FechaModificacion = DateTime.Now;

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Sedes.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            var entidad = await context.Sedes.FirstOrDefaultAsync(x => x.Id == id);
            entidad.Id = id;
            entidad.Estado = false;
            context.Entry(entidad).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> ValidarExistencia(SedeDTOCR sedeDTOCR)
        {
            var ValidarExistencia = await context.Sedes.AnyAsync(x => (x.Nombre == sedeDTOCR.Nombre || x.IdentificacionHominis == sedeDTOCR.IdentificacionHominis));
            return ValidarExistencia;
        }

    }
}
