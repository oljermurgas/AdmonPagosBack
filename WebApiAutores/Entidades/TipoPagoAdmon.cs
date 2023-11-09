using System.ComponentModel.DataAnnotations;
using WebApiAutores.Entidades;

namespace AdminPagosApi.Entidades
{
    public class TipoPagoAdmon
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime FechaModificacion { get; set; } = DateTime.Now;

        public List<TipoObligacion> TipoObligacion { get; set; }
        public List<TipoConceptoFacturacion> TipoConceptoFacturacion { get; set; }
    }
}
