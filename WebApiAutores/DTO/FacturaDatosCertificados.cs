namespace AdminPagosApi.DTO
{
    public class FacturaDatosCertificados
    {
        public int Id { get; set; }
        public string FacturaNumero { get; set; }
        public string FacturaReferenciaPago { get; set; }
        public string FacturaFechaEmision { get; set; }
        public string FacturaFechaOportunoPago { get; set; }
        public string FacturaValor { get; set; }
        public string FacturaValorxConcepto { get; set; }
        public string FacturaNota { get; set; }
        public int FacturaEstadoId { get; set; }
        public string FacturaEstadoCodigo { get; set; }
        public string FacturaEstadoDescripcion { get; set; }
        public string FacturaPeriodoInicio { get; set; }
        public string FacturaPeriodoFin { get; set; }
        public string FacturaPagoInmediato { get; set; }
        public int EntidadId { get; set; }
        public string EntidadNombre { get; set; }
        public string EntidadIdentificacion { get; set; }
        public int CoordinacionPGNId { get; set; }
        public string CoordinacionPGNNombre { get; set; }
        public string SedeId { get; set; }
        public string SedeNombre { get; set; }
        public string SedeDireccion { get; set; }
        public int TipoInmuebleId { get; set; }
        public string SedeTipoInmueble { get; set; }
        public string SedeDepartamento { get; set; }
        public string SedeMunicipo { get; set; }
        public string NumeroContratoEntidad { get; set; }
        public int TipoObligacionId { get; set; }
        public string TipoObligacionNombre { get; set; }
        public string TipoObligacionTotal { get; set; }
        public string TipoObligacionRubroPresupuestal { get; set; }
        public string TipoObligacionCodigo { get; set; }
        public string TipoObligacionConsumo { get; set; }
        public int TipoPagoAdmonId { get; set; }    
        public string RubroPresupuesto { get; set; }    
    }
}
