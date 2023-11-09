using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace AdminPagosApi.Entidades
{
    public class FacturaEstado
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Descripcion { get; set; }
        public string ColorLetra { get; set; }
        public string ColorFondo { get; set; }  
        public bool Estado { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
