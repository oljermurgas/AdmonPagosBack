using AdminPagosApi.DTO;
using AdminPagosApi.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAutores.Entidades;

namespace AdminPagosApi.Helpers
{
    public class AutoMapperProfiles: Profile  
    {
        public AutoMapperProfiles()
        {
            CreateMap<TipoVinculacionContractual, TipoVinculacionContractualDTO>().ReverseMap();
            CreateMap<TipoVinculacionContractualDTOCR, TipoVinculacionContractual>();

            CreateMap<TipoTarifa, TipoTarifaDTO>().ReverseMap();
            CreateMap<TipoTarifaDTOCR, TipoTarifa>();

            CreateMap<TipoPagoAdmon, TipoPagoAdmonDTO>().ReverseMap();
            CreateMap<TipoPagoAdmonDTOCR, TipoPagoAdmon>();

            CreateMap<TipoObligacion, TipoObligacionDTO>().ReverseMap();
            CreateMap<TipoObligacionDTOCR, TipoObligacion>();

            CreateMap<TipoInmueble, TipoInmuebleDTO>().ReverseMap();
            CreateMap<TipoInmuebleDTOCR, TipoInmueble>();

            CreateMap<TipoEmpresa, TipoEmpresaDTO>().ReverseMap();
            CreateMap<TipoEmpresaDTOCR, TipoEmpresa>();

            CreateMap<TipoEmpresaSector, TipoEmpresaSectorDTO>().ReverseMap();
            CreateMap<TipoEmpresaSectorDTOCR, TipoEmpresaSector>();

            CreateMap<TipoEmpresaNivel, TipoEmpresaNivelDTO>().ReverseMap();
            CreateMap<TipoEmpresaNivelDTOCR, TipoEmpresaNivel>();

            CreateMap<TipoConceptoFacturacion, TipoConceptoFacturacionDTO>().ReverseMap();
            CreateMap<TipoConceptoFacturacionDTOCR, TipoConceptoFacturacion>();

            CreateMap<TipoCanalEnvioFactura, TipoCanalEnvioFacturaDTO>().ReverseMap();
            CreateMap<TipoCanalEnvioFacturaDTOCR, TipoCanalEnvioFactura>();

            CreateMap<Departamento, DepartamentoDTO>().ReverseMap();
            CreateMap<Municipio, MunicipioDTO>().ReverseMap();

            CreateMap<Sede, SedeDTO>().ReverseMap();
            CreateMap<SedeDTOCR, Sede>();

            CreateMap<SedeContrato, SedeContratoDTO>().ReverseMap();
            CreateMap<SedeContratoDTOCR, SedeContrato>();

            CreateMap<Entidad, EntidadDTO>().ReverseMap();
            CreateMap<EntidadDTOCR, Entidad>();

            CreateMap<EntidadTipoObligacion, EntidadTipoObligacionDTO>()
                    .ForMember(dest => dest.TipoObligacion, opt => opt.MapFrom(src => src.TipoObligaciones))
                     .ReverseMap();

            CreateMap<EntidadTipoObligacionDTOCR, EntidadTipoObligacion>();

            CreateMap<FacturaRegistro, FacturaRegistroDTO>().ReverseMap();
            CreateMap<FacturaRegistroDTOCR, FacturaRegistro>();

            CreateMap<SedeEntidad, SedeEntidadDTO>().ReverseMap();
            CreateMap<SedeEntidadDTOCR, SedeEntidad>();

            CreateMap<CoordinacionPGN, CoordinadorPgnDTO>().ReverseMap();
            CreateMap<CoordinadorPgnDTOCR, CoordinacionPGN>();

            CreateMap<CoordinacionPGNSede, CoordnadorPgnSedeDTO>()
                .ForMember(dest => dest.NombreSede, opt => opt.MapFrom(src => src.Sedes.Nombre))
                .ReverseMap();
            CreateMap<CoordnadorPgnSedeDTOCR, CoordinacionPGNSede>();

            CreateMap<FacturaTipoObligacionConceptos, FacturaTipoObligacionConceptosDTO>().ReverseMap();
            CreateMap<FacturaTipoObligacionConceptosDTOCR, FacturaTipoObligacionConceptos>();


        }
    }
}
