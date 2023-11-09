using System.ComponentModel.DataAnnotations;
using WebApiAutores.Entidades;

namespace AdminPagosApi.Entidades
{
    public class TipoObligacion //Energia | Acueducto | Gas | Impuesto Renta
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
        public int TipoPagoAdmonId { get; set; }
        public TipoPagoAdmon TipoPagoAdmon { get; set; }
        public string imagen { get; set; } 
    }
}
