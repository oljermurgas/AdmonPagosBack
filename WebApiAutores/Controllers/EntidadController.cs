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
    [Route("AdmonPago/Api/Entidad")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EntidadController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public EntidadController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<EntidadDTO>>> Get()
        {
            var entidades = await context.Entidades
                                 .OrderBy(e => e.Nombre)
                                 .ToListAsync();
            var dtos = mapper.Map<List<EntidadDTO>>(entidades);
            return dtos;
        }

        //[HttpGet("{codigo}")]
        //public async Task<ActionResult<EntidadDTO>> Get(string nombre)
        //{
        //    var entidades = await context.Entidades.FirstOrDefaultAsync(x => x.Nombre.Contains(nombre));
        //    if (entidades == null)
        //    {
        //        return NotFound();
        //    }
        //    var dtos = mapper.Map<EntidadDTO>(entidades);
        //    return dtos;
        //}

        [HttpGet("{id:int}", Name = "obtenerentidad")]
        public async Task<ActionResult<EntidadDTO>> Get(int id)
        {
            var entidad = await context.Entidades.FirstOrDefaultAsync(x => x.Id == id);
            if (entidad == null)
            {
                return NotFound();
            }
            var dtos = mapper.Map<EntidadDTO>(entidad);
            return dtos;
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EntidadDTOCR entidadDTOCR)
        {
            try
            {
                var ValideExistencia = await ValidarExistencia(entidadDTOCR);
                if (ValideExistencia)
                {
                    return BadRequest(new { message = "La entidad : >> " + entidadDTOCR.Nombre + " << ya existe" });
                }

                //var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
                var entidad = mapper.Map<Entidad>(entidadDTOCR);
                //-----------------------------------------------------------------------------------------
                //entidad.UserName = userName;
                entidad.FechaCreacion = DateTime.Now;
                entidad.Estado = true;
                //-----------------------------------------------------------------------------------------
                context.Add(entidad);
                await context.SaveChangesAsync();
                var entidadDTO = mapper.Map<EntidadDTO>(entidad);
                return new CreatedAtRouteResult("obtenerentidad", new { id = entidadDTO.Id }, entidadDTO);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] EntidadDTO entidadDTO)
        {
            var existe = await context.Entidades.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            //var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            var entidad = mapper.Map<Entidad>(entidadDTO);
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
        public async Task<ActionResult> Patch(int id, JsonPatchDocument<EntidadDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }
            var entidadDB = await context.Entidades.FirstOrDefaultAsync(x => x.Id == id);

            if (entidadDB == null)
            {
                return NotFound();
            }

            var entidadDTO = mapper.Map<EntidadDTO>(entidadDB);
            patchDocument.ApplyTo(entidadDTO, ModelState);

            var esValido = TryValidateModel(entidadDTO);
            if (!esValido)
            {
                return BadRequest(ModelState);
            }

            mapper.Map(entidadDTO, entidadDB);
            entidadDB.FechaModificacion = DateTime.Now;

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

        private async Task<bool> ValidarExistencia(EntidadDTOCR entidadDTOCR)
        {
            var ValidarExistencia = await context.Entidades.AnyAsync(x => (x.Identificacion == entidadDTOCR.Identificacion));
            return ValidarExistencia;
        }

    }
}
