using AdminPagosApi.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPagosApi.DTO
{
    public class TipoVinculacionContractualDTOCR
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
    }

    public class TipoTarifaDTOCR
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
    }

    public class TipoPagoAdmonDTOCR
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
    }

    public class TipoObligacionDTOCR
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int TipoPagoAdmonId { get; set; }
        public TipoPagoAdmon TipoPagoAdmon { get; set; }
    }

    public class TipoInmuebleDTOCR
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
    }

    public class TipoEmpresaSectorDTOCR
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
    }

    public class TipoEmpresaNivelDTOCR
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
    }

    public class TipoConceptoFacturacionDTOCR
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int TipoPagoAdmonId { get; set; }
        public TipoPagoAdmon TipoPagoAdmon { get; set; }
    }

    public class TipoCanalEnvioFacturaDTOCR
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
    }

    public class TipoEmpresaDTOCR
    {
        public string Codigo { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Descripcion { get; set; }
    }

}
