using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AdminPagosApi.Entidades
{
    public class MMenuRol
    {
        public int id { get; set; }
        [Required]
        [Column(TypeName = "NVARCHAR(256)")]
        public string Rol { get; set; }
        public int MMenuId { get; set; }
        public bool Activo { get; set; }
        public string UserName { get; set; }
        public MMenu MMenu { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime FechaModificacion { get; set; } = DateTime.Now;
    }
}
