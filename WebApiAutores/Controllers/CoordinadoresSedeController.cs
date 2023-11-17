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
    [Route("AdmonPago/Api/CoordinadorsSede")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CoordinadoresSedeController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CoordinadoresSedeController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CoordinadorPgnDTO>>> Get()
        {
            var entidades = await context.CoordinacionPGNs
                                 .OrderBy(e => e.Coodinacion)
                                 .ToListAsync();
            var dtos = mapper.Map<List<CoordinadorPgnDTO>>(entidades);
            return dtos;
        }


        [HttpGet("{obtenercoordinacionsede}")]
        public async Task<ActionResult<CoordnadorPgnSedeDTO>> Get(int  id)
        {
            var entidades = await context.CoordinacionPGNSedes.FirstOrDefaultAsync(x => x.Id == id);
            if (entidades == null)
            {
                return NotFound();
            }
            var dtos = mapper.Map<CoordnadorPgnSedeDTO>(entidades);
            return dtos;
        }

        [HttpGet("list/{id:int}")]
        public async Task<ActionResult<List<CoordinadorPgnDTO>>> GetList(int id)
        {
            var entidades = await context.CoordinacionPGNs
          //      .Include(x => x.coo)
                .Where(x => x.Id == id).ToListAsync();

            if (!entidades.Any())
            {
                return NotFound();
            }

            var dtos = mapper.Map<List<CoordinadorPgnDTO>>(entidades);
            return Ok(dtos);
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CoordnadorPgnSedeDTOCR coordnadorPgnSedeDTOCR)
        {
            try
            {
                var ValideExistencia = await ValidarExistencia(coordnadorPgnSedeDTOCR);
                if (ValideExistencia)
                {
                    return BadRequest(new { message = "La sede : >> " + coordnadorPgnSedeDTOCR.SedeId + " << ya existe" });
                }

                //var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
                var entidad = mapper.Map<CoordinacionPGNSede>(coordnadorPgnSedeDTOCR);
                //-----------------------------------------------------------------------------------------
                //entidad.UserName = userName;
                entidad.FechaCreacion = DateTime.Now;

                //-----------------------------------------------------------------------------------------
                context.Add(entidad);
                await context.SaveChangesAsync();
                var entidadDTO = mapper.Map<CoordnadorPgnSedeDTO>(entidad);
                return new CreatedAtRouteResult("obtenercoordinacionsede", new { id = entidadDTO.Id }, entidadDTO);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] CoordinadorPgnDTO coordinadorDTO)
        {
            var existe = await context.CoordinacionPGNs.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            //var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            var entidad = mapper.Map<CoordinadorPgnDTO>(coordinadorDTO);
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
        public async Task<ActionResult> Patch(int id, JsonPatchDocument<CoordinadorPgnDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }
            var sedeDB = await context.CoordinacionPGNs.FirstOrDefaultAsync(x => x.Id == id);

            if (sedeDB == null)
            {
                return NotFound();
            }

            var sedeDTO = mapper.Map<CoordinadorPgnDTO>(sedeDB);
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
            var existe = await context.CoordinacionPGNs.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            var entidad = await context.CoordinacionPGNs.FirstOrDefaultAsync(x => x.Id == id);
            entidad.Id = id;
            entidad.Estado = false;
            context.Entry(entidad).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> ValidarExistencia(CoordnadorPgnSedeDTOCR coordnadorPgnSedeDTOCR)
        {
            var ValidarExistencia = await context.CoordinacionPGNSedes.AnyAsync(x => (x.SedeId == coordnadorPgnSedeDTOCR.SedeId && x.CoordinacionPGNId == coordnadorPgnSedeDTOCR.CoordinacionPGNId));
            return ValidarExistencia;
        }

    }
}
