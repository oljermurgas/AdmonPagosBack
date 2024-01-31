using System.ComponentModel.DataAnnotations;

namespace AdminPagosApi.DTO
{
    public class FirmasDTOCR
    {

        [Required(ErrorMessage = "Este campo es requerido")]
        public string Descripcion { get; set; }

        public string FuncionarioElaboro { get; set; }
        public string FuncionarioElaboroCargo { get; set; }
        public string FuncionarioElaboroDependencia { get; set; }
        public IFormFile FuncionarioElaboroImagenFirma { get; set; }
        public string FuncionarioAprueba { get; set; }
        public string FuncionarioApruebaCargo { get; set; }
        public string FuncionarioApruebaDependencia { get; set; }
        public IFormFile FuncionarioApruebaImagenFirma { get; set; }

        public int TipoPagoAdmonId { get; set; }
    }
}
