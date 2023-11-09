using System.ComponentModel.DataAnnotations;

namespace AdminPagosApi.Entidades
{
    public class TipoEmpresaNivel
    {
        public int Id { get; set; }
        public string Codigo { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
