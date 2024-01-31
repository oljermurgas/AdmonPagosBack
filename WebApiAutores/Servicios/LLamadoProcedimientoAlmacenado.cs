using AdminPagosApi.DTO;
using AdminPagosApi.Entidades;
using Microsoft.AspNetCore.Connections;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AdminPagosApi.Servicios
{

    public class LLamadoProcedimientoAlmacenado : ILlamadoProcedimientoAlmacenado
    {
        private readonly string connectionString;

        public LLamadoProcedimientoAlmacenado(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("defaultConnection");
        }

        public async Task<IEnumerable<FacturaDatosCertificados>> ObtenerDatosFacturasParaCertificados(string NumFactura, string NumCertificado, string FechaInicio,
                                            string FechaFinal, int CoordinacionId, int SedeId, int EntidadId,
                                            int TipoObligacionId, int PaginaActual, int PaginaCantidadMostrar)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var query = "SP_ObtenerDatosFacturasParaCertificados";
                var parameters = new DynamicParameters();
                parameters.Add("@NumFactura", NumFactura);
                parameters.Add("@NumCertificado", NumCertificado);
                parameters.Add("@FechaInicio", FechaInicio);
                parameters.Add("@FechaFinal", FechaFinal);
                parameters.Add("@CoordinacionId", CoordinacionId);
                parameters.Add("@SedeId", SedeId);
                parameters.Add("@EntidadId", EntidadId);
                parameters.Add("@TipoObligacionId", TipoObligacionId);
                parameters.Add("@PaginaActual", PaginaActual);
                parameters.Add("@PaginaCantidadMostrar", PaginaCantidadMostrar);

                var registros = await connection.QueryAsync<FacturaDatosCertificados>(query, parameters, commandType: CommandType.StoredProcedure);
                return registros;
            }
        }

    }
}


    