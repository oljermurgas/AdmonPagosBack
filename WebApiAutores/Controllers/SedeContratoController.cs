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
    [Route("AdmonPago/Api/SedeContrato")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SedeContratoController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public SedeContratoController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<SedeContratoDTO>>> Get()
        {
            var entidades = await context.SedeContratos
                                 .OrderBy(e => e.FechaInicio)
                                 .ToListAsync();
            var dtos = mapper.Map<List<SedeContratoDTO>>(entidades);
            return dtos;
        }

        //[httpget("{codigo}")]
        //public async task<actionresult<sedecontratodto>> get(int codigo)
        //{
        //    var entidades = await context.sedecontratos.firstordefaultasync(x => x.sedeid == codigo);
        //    if (entidades == null)
        //    {
        //        return notfound();
        //    }
        //    var dtos = mapper.map<sedecontratodto>(entidades);
        //    return dtos;
        //}

        [HttpGet("{id:int}")]
        public async Task<ActionResult<List<SedeContratoDTO>>> Get(int id)
        {
            var entidades = await context.SedeContratos.Where(x => x.SedeId == id).ToListAsync();

            if (!entidades.Any())
            {
                return NotFound();
            }

            var dtos = mapper.Map<List<SedeContratoDTO>>(entidades);
            return Ok(dtos);
        }



        //[HttpGet("{id:int}", Name = "obtenersedecontrato")]
        //public async Task<ActionResult<List<SedeContratoDTO>>> Get(int id)
        //{
        //    var entidad = await context.SedeContratos.FirstOrDefaultAsync(x => x.Id == id);
        //    if (entidad == null)
        //    {
        //        return NotFound();
        //    }
        //    var dtos = mapper.Map<List<SedeContratoDTO>>(entidad);
        //    return dtos;
        //}

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] SedeContratoDTOCR sedeContratoDTOCR)
        {
            try
            {
                var ValideExistencia = await ValidarExistencia(sedeContratoDTOCR);
                if (ValideExistencia)
                {
                    return BadRequest(new { message = "La sede : >> " + sedeContratoDTOCR.DocumentoNumero + " << ya existe" });
                }

                //var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
                var entidad = mapper.Map<SedeContrato>(sedeContratoDTOCR);
                //-----------------------------------------------------------------------------------------
                //entidad.UserName = userName;
                entidad.FechaCreacion = DateTime.Now;
                entidad.Estado = true;
                //-----------------------------------------------------------------------------------------
                context.Add(entidad);
                await context.SaveChangesAsync();
                var entidadDTO = mapper.Map<SedeContratoDTO>(entidad);
                return new CreatedAtRouteResult("obtenersedecontrato", new { id = entidadDTO.Id }, entidadDTO);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] SedeContratoDTO sedeContratoDTO)
        {
            var existe = await context.SedeContratos.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            //var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            var entidad = mapper.Map<SedeContrato>(sedeContratoDTO);
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
        public async Task<ActionResult> Patch(int id, JsonPatchDocument<SedeContratoDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }
            var entidadDB = await context.SedeContratos.FirstOrDefaultAsync(x => x.Id == id);

            if (entidadDB == null)
            {
                return NotFound();
            }

            var entidadDTO = mapper.Map<SedeContratoDTO>(entidadDB);
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
            var existe = await context.SedeContratos.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            var entidad = await context.SedeContratos.FirstOrDefaultAsync(x => x.Id == id);
            entidad.Id = id;
            entidad.Estado = false;
            context.Entry(entidad).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> ValidarExistencia(SedeContratoDTOCR sedeContratoDTOCR)
        {
            var ValidarExistencia = await context.SedeContratos.AnyAsync(x => (x.DocumentoNumero == sedeContratoDTOCR.DocumentoNumero && x.SedeId == sedeContratoDTOCR.SedeId));
            return ValidarExistencia;
        }

    }
}
