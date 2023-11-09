using AdminPagosApi.Entidades;
using System.ComponentModel.DataAnnotations;
using WebApiAutores.Entidades;

namespace AdminPagosApi.DTO
{
    public class SedeContratoDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public string DocumentoNumero { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFinal { get; set; }

        public string TerceroIdentificacion { get; set; }

        public string TerceroNombres { get; set; }
        public string TerceroApellidos { get; set; }
        public string Notas { get; set; }
        public string LinkSecop { get; set; }
        public decimal Valor { get; set; }
        public int Meses { get; set; }
        public bool Estado { get; set; }

        public int SedeId { get; set; }

    }
}
