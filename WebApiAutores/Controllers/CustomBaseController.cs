using AdminPagosApi.DTO;
using AdminPagosApi.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPagosApi.Controllers
{
    public class CustomBaseController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CustomBaseController(ApplicationDbContext context, IMapper mapper) 
        {
            this.context = context;
            this.mapper = mapper;
        }

        //protected async Task<List<TDTO>> Get<TEntidad, TDTO>(PaginacionDTO paginacionDTO, IQueryable<TEntidad>queryable) where TEntidad : class
        //{
        //    await HttpContext.InsertarParametrosPaginacion(queryable, paginacionDTO.CantidadRegistrosPorPagina);
        //    var entidades = await queryable.Paginar(paginacionDTO).ToListAsync();
        //    return mapper.Map<List<TDTO>>(entidades); ;
        //}

        //protected async Task<List<TDTO>> Get<TEntidad, TDTO>(DTOs.PaginacionDTO paginationDTO) where TEntidad : class
        //{
        //    var queryable = context.Set<TEntidad>().AsQueryable();
        //    return await Get<TEntidad, TDTO>(paginationDTO, queryable);
        //}


        //protected async Task<List<TDTO>> Get<TEntidad, TDTO>(DTOs.PaginacionDTO paginationDTO) where TEntidad : class
        //{
        //    var entidades = await context.Set<TEntidad>().ToListAsync();
        //    var dtos = mapper.Map<List<TDTO>>(entidades);
        //    return dtos;
        //}

    }
}
