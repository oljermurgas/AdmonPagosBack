using System.ComponentModel.DataAnnotations;

namespace AdminPagosApi.Entidades
{
    public class TipoInmueble
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [StringLength(30, ErrorMessage = "Máximo 30 caracteres.")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string Descripcion { get; set; }

        public bool Estado { get; set; }
        public int UsuarioId { get; set; }       
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }

        // Constructor por defecto
        public TipoInmueble()
        {
            FechaCreacion = DateTime.Now;
            FechaModificacion = DateTime.Now;
        }
    }
}
