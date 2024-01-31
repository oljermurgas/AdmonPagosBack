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
using AdminPagosApi;
using AdminPagosApi.Validaciones;

namespace AdminPagosApi.Controllers
{
    [ApiController]
    [Route("admonpago/api/scuentas")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy ="esAdmin")]
    public class SCuentasController: CustomBaseController 
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _singInManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        //private readonly IValidarPermiso sp_validarPermiso;

        public SCuentasController(  UserManager<ApplicationUser> userManager, 
                                    SignInManager<ApplicationUser> singInManager,
                                    IConfiguration configuration, 
                                    ApplicationDbContext context,
                                    IMapper mapper
                                    //IValidarPermiso sp_validarPermiso 
                                    ): base(context, mapper)
                                    {
                                        _userManager = userManager; 
                                        _singInManager = singInManager;
                                        _configuration = configuration; 
                                        this.context = context;
                                        this.mapper = mapper;
                                        //this.sp_validarPermiso = sp_validarPermiso;
                                    }


        [HttpPost("crear")]
        [Authorize(Policy = "esAdmin")]
        public async Task<ActionResult<RespuestaAutenticacion>> crear(CredencialesUsuario credencialesUsuario)
        {
            try
            {
                var usuario = new ApplicationUser
                {
                    UserName = credencialesUsuario.Email,
                    Email = credencialesUsuario.Email
                };
                var resultado = await _userManager.CreateAsync(usuario, credencialesUsuario.Password);

                if (resultado.Succeeded)
                    return await ConstruirToken(credencialesUsuario);
                else
                    return BadRequest(resultado.Errors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<RespuestaAutenticacion>> login([FromBody] CredencialesUsuario credencialesUsuario)
        {
            try
            {
                var result = await _singInManager.PasswordSignInAsync(credencialesUsuario.Email,
                                   credencialesUsuario.Password, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    // validar usuario activo y fecha vigencia    
                    return  await ConstruirToken(credencialesUsuario);
                }
                else
                   return BadRequest("Login Incorrecto");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("RenovarToken")]
        public async Task<ActionResult<RespuestaAutenticacion>> Renovar()
        {
            var emailclaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            var email = emailclaim.Value;
            var credencialesUsuario = new CredencialesUsuario()
            {
                Email = email
            };
            return await ConstruirToken(credencialesUsuario);
        }


        //    [HttpPost("LoginResetear")]
        //    public async Task<ActionResult<UserToken>> LoginResetear([FromBody] SUsuarioDTOCRUP userinfo)
        //    {
        //        try
        //        {
        //            var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
        //            bool isAdmin = await sp_validarPermiso.ValidarPermisoAdministrador(userName);
        //            if (isAdmin) // Validar usuario perfil Rol Admin
        //            {
        //                ApplicationUser user = await _userManager.FindByNameAsync(userinfo.UserName.ToUpper());
        //                var resp = await context.Users.AnyAsync(x => x.UserName == userinfo.UserName);
        //                if (resp)
        //                {
        //                    string code = await _userManager.GeneratePasswordResetTokenAsync(user);
        //                    var result = await _userManager.ResetPasswordAsync(user, code, userinfo.Password);
        //                    if (result.Succeeded)
        //                    { return Ok(new { message = "Contraseña Cambiada" }); }
        //                    else
        //                    { return BadRequest(new { message = "Error al generar la contraseña" }); }
        //                }
        //                else
        //                { return BadRequest(new { message = "El usuario >> " + userinfo.UserName + " << no existe" }); }
        //            }
        //            else
        //            { return BadRequest(new { message = "No tiene permiso de Administrador" }); }
        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest(ex.Message);
        //        }
        //    }

        //    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //    [HttpPut("CambiarPaswword")]
        //    public async Task<IActionResult> CambiarPaswword([FromBody] UsuarioCambiarPasswordDTO usuariocambiarDTO)
        //    {
        //        try
        //        {
        //            var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
        //            var result = await _singInManager.PasswordSignInAsync(userName, usuariocambiarDTO.passwordAnterior, isPersistent: false, lockoutOnFailure: false);
        //            if (result.Succeeded)
        //            {
        //                var user = await _userManager.GetUserAsync(User);
        //                var result1 = await _userManager.ChangePasswordAsync(user, usuariocambiarDTO.passwordAnterior, usuariocambiarDTO.nuevaPassword);
        //                if (result1.Succeeded)
        //                {
        //                    return Ok(new { message = "La contraseña ha sido cambiada" });
        //                }
        //                else
        //                {
        //                    return BadRequest(result1.Errors);
        //                }
        //            }
        //            else
        //            {
        //                ModelState.AddModelError(string.Empty, "Usuario y/o contraseña a cambiar estan errados");
        //                return BadRequest(ModelState);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest(ex.Message);
        //        }
        //    }

        private async Task<RespuestaAutenticacion> ConstruirToken(CredencialesUsuario credencialesUsuario)
        {
            var claims = new List<Claim>()
                        {   new Claim("email", credencialesUsuario.Email),
                            new Claim("miValor",credencialesUsuario.Email)  //El Valor que uno quiera
                            //new Claim(ClaimTypes.Email, userInfo.Email)
                        };

            var usuario = await _userManager.FindByEmailAsync(credencialesUsuario.Email);
            claims.Add(new Claim(ClaimTypes.NameIdentifier, usuario.Id));
            var ClaimsBD = await _userManager.GetClaimsAsync(usuario);
            claims.AddRange(ClaimsBD);
                       
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(1);
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                //expires: DateTime.Now.AddSeconds(10),
                expires: expiration,
                signingCredentials: creds);

            return new RespuestaAutenticacion()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }


        [HttpPost("HacerAdmin")]
        public async Task<ActionResult> HacerAdmin(EditarAdminDTO editarAdminDTO)
        {
            var usuario = await _userManager.FindByEmailAsync(editarAdminDTO.Email);
            await _userManager.AddClaimAsync(usuario, new Claim("esAdmin", "1"));
            return NoContent();
        }

        [HttpPost("RemoverAdmin")]
        public async Task<ActionResult> RemoverAdmin(EditarAdminDTO editarAdminDTO)
        {
            var usuario = await _userManager.FindByEmailAsync(editarAdminDTO.Email);
            await _userManager.RemoveClaimAsync(usuario, new Claim("esAdmin", "1"));
            return NoContent();
        }

        [HttpGet("listaUsuarios")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<SUsuarioDTO>>> Get([FromQuery] PaginacionDTO paginationDTO)
        {
            try
            {
                var entidades = await context.Users.OrderBy(x => x.Email).ThenBy(x => x.Email)
                                                   .ToListAsync();
                var dtos = mapper.Map<List<SUsuarioDTO>>(entidades);
                return dtos;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(id.ToUpper());
                if (user != null)
                {
                    //user.Activo = false;
                    context.Entry(user).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                    return Ok(new { message = "Actualizado los datos" });
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Put(string id, [FromBody] SUsuarioDTOCRUP usuarioUD)
        {
            try
            {
                var UserNombre = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
                ApplicationUser user = await _userManager.FindByNameAsync(id.ToUpper());
                var resp = await context.Users.AnyAsync(x => x.UserName == id.ToUpper());
                if (resp)
                {
                    //user.Apellido = usuarioUD.Apellido;
                    //user.Nombre = usuarioUD.Nombre;
                    //user.Activo = usuarioUD.Activo;
                    //user.Email = usuarioUD.Email;
                    //user.FechaVigencia = usuarioUD.FechaVigencia;
                    //user.TipoUsuario = usuarioUD.TipoUsuario;
                    //user.UsuarioCreador = UserNombre;
                    context.Entry(user).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                    return Ok(new { message = "Actualizado los datos" });
                }
                else
                { return BadRequest(new { message = "El usuario >> " + id + " << no existe" }); }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private async Task<bool> ValidarExistencia(CredencialesUsuario credencialesUsuario)
        {
            var ValidarExistencia = await context.Users.AnyAsync(x => x.Email == credencialesUsuario.Email);
            return ValidarExistencia;
        }
    }
}
