using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPagosApi.Entidades
{
    public class UUserRol
    {
        [Required]
        public string UsuarioId { get; set; }
        [Required]
        public string Rol { get; set; }
    }
}
