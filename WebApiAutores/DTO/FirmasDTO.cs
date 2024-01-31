using System.ComponentModel.DataAnnotations;

namespace AdminPagosApi.DTO
{
    public class FirmasDTO
    {
      
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string Descripcion { get; set; }

        public string FuncionarioElaboro { get; set; }
        public string FuncionarioElaboroCargo { get; set; }
        public string FuncionarioElaboroDependencia { get; set; }
        public string FuncionarioElaboroFirma { get; set; }
        public string FuncionarioAprueba { get; set; }
        public string FuncionarioApruebaCargo { get; set; }
        public string FuncionarioApruebaDependencia { get; set; }
        public string FuncionarioApruebaFirma { get; set; }

        public bool Estado { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int TipoPagoAdmonId { get; set; }
    }
}
