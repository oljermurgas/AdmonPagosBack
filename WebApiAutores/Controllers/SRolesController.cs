using AdminPagosApi.Entidades;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;
using AdminPagosApi.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using AdminPagosApi.Validaciones;

namespace AdminPagosApi.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/sroles")]
    public class SRolesController : CustomBaseController //ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _rolManager;

        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IValidarPermiso sp_validarPermiso;

        public SRolesController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> rolManager,
                                 ApplicationDbContext context,
                                 IMapper mapper, IValidarPermiso sp_validarPermiso) : base(context, mapper)
        {
            _userManager = userManager;
            _rolManager = rolManager;
            this.context = context;
            this.mapper = mapper;
            this.sp_validarPermiso = sp_validarPermiso;
        }

        [HttpPost("Crear")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateRol([FromBody] URol model)
        {
            try
            {
                if (model == null || string.IsNullOrEmpty(model.RolName))
                    return null;

                var ValideExistencia = await ValidarExistencia(model);
                if (ValideExistencia)
                   return BadRequest(new { message = "El Rol >> " + model.RolName + " << ya existe" });
                
                await _rolManager.CreateAsync(new IdentityRole(model.RolName));
                var validarCreacion = await _rolManager.FindByNameAsync(model.RolName);
                if (validarCreacion == null)
                   return BadRequest(new { message = "Error crear el Rol: >> " + model.RolName + " << " });               
                else
                   return Ok(new { message = "Creado el rol" }); 
               
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("AsignarUserRol")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AsignarUserRol([FromBody] UUserRol model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.UsuarioId);
                if (user == null)
                    return NotFound();

                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, model.Rol));
                await _userManager.AddToRoleAsync(user, model.Rol);
                return NoContent();
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); 
            }
        }

        [HttpPost("RemoveRol")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> RemoverRol(UUserRol model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.UsuarioId);
                if (user == null)
                    return NotFound();

                await _userManager.RemoveClaimAsync(user, new Claim(ClaimTypes.Role, model.Rol));
                await _userManager.RemoveFromRolesAsync(user, new string[] { model.Rol });
                return Ok(new { message = "Registro eliminado" });
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }
        }

        [HttpGet("GetRoles")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetRoles()
        {
            var entidades = await context.Roles.Select(x => new { RolName = x.Name })
                                                  .OrderBy(x => x.RolName).ToListAsync();
            return Ok(entidades);
        }

        [HttpGet]
        [Route("UsuarioxRoles/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UsuarioxRoles(string id)
        {
            var user = await _userManager.FindByNameAsync(id.ToUpper());
            var rolesUsuario = await _userManager.GetRolesAsync(user);
            var userRoles = rolesUsuario.Select(x => new { Roles = x });
            return Ok(userRoles);
        }

        [HttpGet]
        [Route("GetRolxUsuario/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetRolxUsuario(string id)
        {
            var user = await _userManager.GetUsersInRoleAsync(id);
            return Ok(user);
        }

        private async Task<bool> ValidarExistencia(URol rolinfo)
        {
            var ValidarExistencia = await context.Roles.AnyAsync(x => x.Name == rolinfo.RolName);
            return ValidarExistencia;
        }
    }
}
