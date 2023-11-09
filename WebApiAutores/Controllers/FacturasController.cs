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
    [Route("AdmonPago/Api/Factura")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FacturaController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public FacturaController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<List<FacturaRegistroDTO>>> Get()
        {
            var facturas = await context.FacturaRegistro
               .Include(x => x.FacturasTipoObligacion)
               .ThenInclude(fto => fto.TipoObligacion)
               .Include(x => x.FacturaEstado)
               .Include(x => x.Entidad)
               .Include(x => x.Sede)
               .OrderBy(e => e.FechaPago)
               .ToListAsync();

            var dtos = mapper.Map<List<FacturaRegistroDTO>>(facturas);
            return dtos;
        }



    //[HttpGet]
    //public async Task<ActionResult<List<FacturaRegistroDTO>>> Get()
    //{
    //    var entidades = await context.FacturaRegistro
    //                         .Include(x => x.FacturaEstado)
    //                         .Include(x => x.Entidad)
    //                         .Include(x => x.Sede)
    //                         .Include(x => x.FacturasTipoObligacion)
    //                         .OrderBy(e => e.FechaPago)
    //                         .ToListAsync();
    //    var dtos = mapper.Map<List<FacturaRegistroDTO>>(entidades);
    //    return dtos;
    //}

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

    [HttpGet("{id:int}", Name = "obtenerfactura")]
        public async Task<ActionResult<FacturaRegistroDTO>> Get(int id)
        {
            var entidad = await context.FacturaRegistro.FirstOrDefaultAsync(x => x.Id == id);
            if (entidad == null)
            {
                return NotFound();
            }
            var dtos = mapper.Map<FacturaRegistroDTO>(entidad);
            return dtos;
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] FacturaRegistroDTOCR facturaRegistroDTOCR)
        {
            try
            {
                var ValideExistencia = await ValidarExistencia(facturaRegistroDTOCR);
                if (ValideExistencia)
                {
                    return BadRequest(new { message = "La factura : >> " + facturaRegistroDTOCR.NumeroContrato + " << ya existe" });
                }

                //var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
                var entidad = mapper.Map<FacturaRegistro>(facturaRegistroDTOCR);
                //-----------------------------------------------------------------------------------------
                //entidad.UserName = userName;
                entidad.FechaCreacion = DateTime.Now;
                entidad.Estado = true;
                //-----------------------------------------------------------------------------------------
                context.Add(entidad);
                await context.SaveChangesAsync();
                var entidadDTO = mapper.Map<FacturaRegistroDTO>(entidad);
                return new CreatedAtRouteResult("obtenerfactura", new { id = entidadDTO.Id }, entidadDTO);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] FacturaRegistroDTO facturaRegistroDTO)
        {
            var existe = await context.FacturaRegistro.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            //var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            var entidad = mapper.Map<FacturaRegistro>(facturaRegistroDTO);
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
        public async Task<ActionResult> Patch(int id, JsonPatchDocument<FacturaRegistroDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }
            var entidadDB = await context.FacturaRegistro.FirstOrDefaultAsync(x => x.Id == id);

            if (entidadDB == null)
            {
                return NotFound();
            }

            var entidadDTO = mapper.Map<FacturaRegistroDTO>(entidadDB);
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
            var existe = await context.FacturaRegistro.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            var entidad = await context.FacturaRegistro.FirstOrDefaultAsync(x => x.Id == id);
            entidad.Id = id;
            entidad.Estado = false;
            context.Entry(entidad).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> ValidarExistencia(FacturaRegistroDTOCR facturaRegistroDTOCR)
        {
            var ValidarExistencia = await context.FacturaRegistro.AnyAsync(x => (x.SedeId == facturaRegistroDTOCR.SedeId && x.EntidadId == facturaRegistroDTOCR.EntidadId  && x.FacturaNumero == facturaRegistroDTOCR.FacturaNumero));
            return ValidarExistencia;
        }

    }
}
