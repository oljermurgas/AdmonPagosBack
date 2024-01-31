using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPagosApi.Validaciones
{
    public interface IValidarPermiso
    {
        Task<bool> ValidarPermisoDiaCerrado(string usuarioActual, DateTime fechaDocumento);
        Task<bool> ValidarPermisoAdministrador(string usuarioActual);
    }
}