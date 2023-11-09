using System.ComponentModel.DataAnnotations;

namespace AdminPagosApi.Entidades
{
    public class TipoTarifa
    {
        public int Id { get; set; }
        public string Codigo { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime FechaModificacion { get; set; } = DateTime.Now;
    }
}
