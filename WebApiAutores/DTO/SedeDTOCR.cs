using AdminPagosApi.Entidades;
using System.ComponentModel.DataAnnotations;
using WebApiAutores.Entidades;

namespace AdminPagosApi.DTO
{
    public class SedeDTOCR
    {
        [Required(ErrorMessage = "Este campo es requerido")]
        public string IdentificacionHominis { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string Nombre { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public string CedulaCatastral { get; set; }
        public string MatriculaInnoviliaria { get; set; }

        public int TipoVinculacionContractualId { get; set; } 

        public int TipoInmuebleId { get; set; } 

        public int DepartamentoId { get; set; }

        public int MunicipioId { get; set; }
    }
}
