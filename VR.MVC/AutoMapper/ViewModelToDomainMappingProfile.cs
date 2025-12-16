using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using VR.Application.DTOs;
using VR.Domain.Entities;
using VR.MVC.ViewModels;

namespace VR.MVC.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<FabricanteVeiculoViewModel, FabricanteVeiculo>()
            .ForMember(dest => dest.Endereco, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<VeiculoViewModel, Veiculo>();
            CreateMap<VendaViewModel, Venda>()
            .ForMember(dest => dest.Cliente, opt => opt.Ignore())
            .ForMember(dest => dest.Veiculo, opt => opt.Ignore());


            CreateMap<EnderecoViewModel, Endereco>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.FabricanteVeiculo, opt => opt.Ignore())
            .ForMember(dest => dest.FabricanteVeiculoId, opt => opt.Ignore());

            CreateMap<ClienteViewModel, Cliente>();
            CreateMap<VendaListaViewModel, VendaListaDto>();

            CreateMap<RelatorioVendasMensalDto, RelatorioVendasMensalViewModel>();
            CreateMap<FabricanteRelatorioDto, FabricanteRelatorio>();
            CreateMap<TipoVeiculoRelatorioDto, TipoVeiculoRelatorio>();

        }

    }
}