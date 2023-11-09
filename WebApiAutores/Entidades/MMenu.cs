using System.ComponentModel.DataAnnotations;

namespace AdminPagosApi.Entidades
{
    public class MMenu
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Menu { get; set; }
        public int MenuId { get; set; }
        [StringLength(60)]
        public string Icono { get; set; }
        [StringLength(30)]
        public string TextoAdicional { get; set; }
        [StringLength(60)]
        public string Link { get; set; }
        public bool Activo { get; set; }
        public int Nivel { get; set; }
        public int Orden { get; set; }

        [StringLength(40)]
        public string UserName { get; set; }
        public MMenu MMenuPadre { get; set; }

        public int UsuarioId { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime FechaModificacion { get; set; } = DateTime.Now;
    }
}
