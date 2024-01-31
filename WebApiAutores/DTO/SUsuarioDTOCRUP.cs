using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPagosApi.DTO
{
    public class SUsuarioDTOCRUP
    {
        [StringLength(50)]
        public string Nombre { get; set; }
        [StringLength(50)]
        public string Apellido { get; set; }
        public bool Activo { get; set; } = true;
        public string TipoUsuario { get; set; }
        public DateTime FechaVigencia { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
