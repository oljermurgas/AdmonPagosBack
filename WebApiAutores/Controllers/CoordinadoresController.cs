using AdminPagosApi.DTO;
using AdminPagosApi.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApiAutores;
using WebApiAutores.Entidades;

namespace AdminPagosApi.Controllers
{
    [ApiController]
    [Route("AdmonPago/Api/Coordinadors")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CoordinadoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;

        public CoordinadoresController(ApplicationDbContext context, 
                                        IMapper mapper,
                                        UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
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

        [HttpGet("{codigo}")]
        public async Task<ActionResult<CoordinadorPgnDTO>> Get(string nombre)
        {
            var entidades = await context.CoordinacionPGNs.FirstOrDefaultAsync(x => x.Coodinacion.Contains(nombre));
            if (entidades == null)
            {
                return NotFound();
            }
            var dtos = mapper.Map<CoordinadorPgnDTO>(entidades);
            return dtos;
        }

        [HttpGet("list/{id:int}")]
        public async Task<ActionResult<List<SedeDTO>>> GetList(int id)
        {
            var entidades = await context.CoordinacionPGNSedes
              .Include(x => x.CoordinacionPGNs)
              .Where(x => x.CoordinacionPGNId == id)
              .ToListAsync();
            if (!entidades.Any())
               return NotFound();
            
            var dtos = mapper.Map<List<SedeDTO>>(entidades);
            return Ok(dtos);
        }

        [HttpGet("{id:int}", Name = "obtenercoordinacion")]
        public async Task<ActionResult<CoordinadorPgnDTO>> Get(int id)
        {
            var entidad = await context.CoordinacionPGNs.FirstOrDefaultAsync(x => x.Id == id);
            if (entidad == null)
                return NotFound();
      
            var dtos = mapper.Map<CoordinadorPgnDTO>(entidad);
            return dtos;
        }


        [HttpPost]
        //[Authorize(Policy = "esAdmin")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Post([FromBody] CoordinadorPgnDTOCR coordinadorDTOCR)
        {
            try
            {
                var ValideExistencia = await ValidarExistencia(coordinadorDTOCR);
                if (ValideExistencia)
                    return BadRequest(new { message = "La cordinación : >> " + coordinadorDTOCR.Coodinacion + " << ya existe" });
                
                var usuarioId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                var entidad = mapper.Map<CoordinacionPGN>(coordinadorDTOCR);
                //-----------------------------------------------------------------------------------------
                entidad.UsuarioId = usuarioId;
                entidad.FechaCreacion = DateTime.Now;
                entidad.Estado = true;
                //-----------------------------------------------------------------------------------------
                context.Add(entidad);
                await context.SaveChangesAsync();
                var entidadDTO = mapper.Map<CoordinadorPgnDTO>(entidad);
                return new CreatedAtRouteResult("obtenercoordinacion", new { id = entidadDTO.Id }, entidadDTO);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        [Authorize(Policy = "esAdmin")]
        public async Task<ActionResult> Put(int id, [FromBody] CoordinadorPgnDTO coordinadorDTO)
        {
            var existe = await context.CoordinacionPGNs.AnyAsync(x => x.Id == id);
            if (!existe)
               return NotFound();
            

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
        [Authorize(Policy = "esAdmin")]
        public async Task<ActionResult> Patch(int id, JsonPatchDocument<CoordinadorPgnDTO> patchDocument)
        {
            if (patchDocument == null)
               return BadRequest();
            
            var sedeDB = await context.CoordinacionPGNs.FirstOrDefaultAsync(x => x.Id == id);

            if (sedeDB == null)
                return NotFound();
            
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
        [Authorize(Policy = "esAdmin")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.CoordinacionPGNs.AnyAsync(x => x.Id == id);
            if (!existe)
               return NotFound();
            
            var entidad = await context.CoordinacionPGNs.FirstOrDefaultAsync(x => x.Id == id);
            entidad.Id = id;
            entidad.Estado = false;
            context.Entry(entidad).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> ValidarExistencia(CoordinadorPgnDTOCR coordinadorDTOCR)
        {
            var ValidarExistencia = await context.CoordinacionPGNs.AnyAsync(x => (x.Coodinacion == coordinadorDTOCR.Coodinacion));
            return ValidarExistencia;
        }

    }
}
