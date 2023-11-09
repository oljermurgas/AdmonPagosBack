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
    [Route("AdmonPago/Api/EntidadTipoObligacion")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EntidadTipoObligacionController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public EntidadTipoObligacionController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<EntidadTipoObligacionDTO>>> Get()
        {
            var entidades = await context.EntidadTipoObligaciones
                .OrderBy(e => e.Entidades.Nombre)
                .ToListAsync();

            var dtos = mapper.Map<List<EntidadTipoObligacionDTO>>(entidades);

            return dtos;
        }


        //[HttpGet("{codigo}")]
        //public async Task<ActionResult<EntidadTipoObligacionDTO>> Get(int entidadid)
        //{
        //    var entidades = await context.EntidadTipoObligaciones.FirstOrDefaultAsync(x => x.EntidadId == entidadid);
        //    if (entidades == null)
        //    {
        //        return NotFound();
        //    }
        //    var dtos = mapper.Map<EntidadTipoObligacionDTO>(entidades);
        //    return dtos;
        //}

        [HttpGet("{id:int}", Name = "obtenerentidadtipoobligacion")]
        public async Task<ActionResult<EntidadTipoObligacionDTO>> Get(int id)
        {
            var entidad = await context.EntidadTipoObligaciones.FirstOrDefaultAsync(x => x.Id == id);
            if (entidad == null)
            {
                return NotFound();
            }
            var dtos = mapper.Map<EntidadTipoObligacionDTO>(entidad);
            return dtos;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EntidadTipoObligacionDTOCR entidadTipoObligacionDTOCR)
        {
            try
            {
                var ValideExistencia = await ValidarExistencia(entidadTipoObligacionDTOCR);
                if (ValideExistencia)
                {
                    return BadRequest(new { message = "La entidad con la obligacion: >> " + entidadTipoObligacionDTOCR.NumeroContrato + " << ya existe" });
                }

                //var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
                var entidad = mapper.Map<EntidadTipoObligacion>(entidadTipoObligacionDTOCR);
                //-----------------------------------------------------------------------------------------
                //entidad.UserName = userName;
                entidad.FechaCreacion = DateTime.Now;
                entidad.Estado = true;
                //-----------------------------------------------------------------------------------------
                context.Add(entidad);
                await context.SaveChangesAsync();
                var entidadDTO = mapper.Map<EntidadTipoObligacionDTO>(entidad);
                return new CreatedAtRouteResult("obtenerentidadtipoobligacion", new { id = entidadDTO.Id }, entidadDTO);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] EntidadTipoObligacionDTO entidadTipoObligacionDTO)
        {
            var existe = await context.EntidadTipoObligaciones.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            //var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            var entidad = mapper.Map<EntidadTipoObligacion>(entidadTipoObligacionDTO);
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
        public async Task<ActionResult> Patch(int id, JsonPatchDocument<EntidadTipoObligacionDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }
            var entidadDB = await context.EntidadTipoObligaciones.FirstOrDefaultAsync(x => x.Id == id);

            if (entidadDB == null)
            {
                return NotFound();
            }

            var entidadDTO = mapper.Map<EntidadTipoObligacionDTO>(entidadDB);
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
            var existe = await context.EntidadTipoObligaciones.AnyAsync(x => x.Id == id);
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

        private async Task<bool> ValidarExistencia(EntidadTipoObligacionDTOCR entidadTipoObligacionDTOCR)
        {
            var ValidarExistencia = await context.EntidadTipoObligaciones.AnyAsync(x => (x.EntidadId == entidadTipoObligacionDTOCR.EntidadId && x.TipoObligacionId == entidadTipoObligacionDTOCR.TipoObligacionId));
            return ValidarExistencia;
        }

    }
}
