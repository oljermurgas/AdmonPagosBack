using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AdminPagosApi.Entidades;

namespace AdminPagosApi.Validaciones
{
    public class ValidarPermiso : IValidarPermiso
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext context;     

        public ValidarPermiso(UserManager<ApplicationUser> userManager,    ApplicationDbContext context
            )
        {
            _userManager = userManager;  
            this.context = context;
        }
       

        public async Task<bool> ValidarPermisoDiaCerrado(string usuarioActual, DateTime fechaDocumento)
        {
            ApplicationUser User = await _userManager.FindByNameAsync(usuarioActual);
            var consultarRolAdmin = _userManager.IsInRoleAsync(User, "Admin");
            bool isAdmin = consultarRolAdmin.Result;

            //    if (isAdmin) // Validar usuario perfil Rol Admin
            //        return true;            

            //    var FechaDoc = DateTime.Parse(fechaDocumento.ToString("yyyy/MM/dd") + " 00:00:00");
            //    var ConsultaUltimoCierre = await context.DCierreDiario.FirstOrDefaultAsync(p => p.Fecha == context.DCierreDiario.Max(x => x.Fecha));
            //    var FechaUltimoCierre = ConsultaUltimoCierre.Fecha;

            //    var diferenciaDia = (FechaDoc - FechaUltimoCierre).Days;

            //    if ((diferenciaDia <= 0) || (diferenciaDia > 1)) // Validar usuario perfil Rol Admin            
            //        return false;

            return true;
        }


        public async Task<bool> ValidarPermisoAdministrador(string usuarioActual)
        {           
            ApplicationUser User = await _userManager.FindByNameAsync(usuarioActual);
            var consultarRolAdmin = _userManager.IsInRoleAsync(User, "Admin");
            bool isAdmin = consultarRolAdmin.Result;

            if (isAdmin) // Validar usuario perfil Rol Admin
                return true;

            return false;
        }
        

            
    }
}




