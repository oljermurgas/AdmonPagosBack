using AdminPagosApi.DTO;
using AdminPagosApi.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPagosApi.Servicios
{
   public interface ILlamadoProcedimientoAlmacenado
    {
        Task<IEnumerable<FacturaDatosCertificados>> ObtenerDatosFacturasParaCertificados( string NumFactura, string NumCertificado, string FechaInicio,
                                            string FechaFinal, int CoordinacionId, int SedeId, int EntidadId,
                                            int TipoObligacionId, int PaginaActual, int PaginaCantidadMostrar );

        //Task<List<TProductoDisponibleXProducto>> SDisponibilidadxProductoxBodega(int ClaseMovId);

        //Task<List<PPersona>> SConsultaPersona(string Nombre1, string Nombre2, string  Apellido1, string Apellido2,
        //                       string Cedula, string Celular);

    }
}

