using AdminPagosApi.Entidades;
using System.ComponentModel.DataAnnotations;
using WebApiAutores.Entidades;

namespace AdminPagosApi.DTO
{
    public class SedeContratoDTO
    {
        public int Id { get; set; }
        public string DocumentoNumero { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFinal { get; set; }
        public string Identificacion { get; set; }
        public string RazonSocial { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public decimal Valor { get; set; }

        public string Notas { get; set; }
        public string LinkSecop { get; set; }

        public bool Estado { get; set; }

        public int SedeId { get; set; }

        public int UsuarioId { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime FechaModificacion { get; set; } = DateTime.Now;

    }
}
