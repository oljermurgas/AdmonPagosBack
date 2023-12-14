using AdminPagosApi.Entidades;
using System.ComponentModel.DataAnnotations;
using WebApiAutores.Entidades;

namespace AdminPagosApi.DTO
{
    public class EntidadDTOCR
    {

        [Required(ErrorMessage = "Este campo es requerido")]
        public string Identificacion { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string Nombre { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }
        public string Nivel { get; set; }

        public int TipoEmpresaId { get; set; } // Natural | Juridica

        public int TipoEmpresaSectorId { get; set; } // Industrial | Minero | Impuesto | Privada

        public int TipoEmpresaNivelId { get; set; } //Municipal | Departamantal | Nacional

        public int DepartamentoId { get; set; }

        public int MunicipioId { get; set; }
        public int PeriodicidadFactura { get; set; }

    }
}
