namespace AdminPagosApi.Entidades
{
    public class FacturaTipoObligacionConceptos
    {
        public int Id { get; set; }
        public int FacturaTipoObligacionId { get; set; }
        public FacturaTipoObligacion FacturaTipoObligacion { get; set; }
        public int TipoConceptoFacturacionId { get; set; }  
        public TipoConceptoFacturacion TipoConceptoFacturacion { get; set; }
        public decimal Valor { get; set; }
        public string Nota { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
