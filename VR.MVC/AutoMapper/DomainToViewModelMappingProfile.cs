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
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<FabricanteVeiculo, FabricanteVeiculoViewModel>();
            CreateMap<Veiculo, VeiculoViewModel>();
        
               CreateMap<Venda, VendaViewModel>()
                .ForMember(dest => dest.FabricanteVeiculoId,
                    opt => opt.MapFrom(src =>
                        src.Veiculo != null
                            ? src.Veiculo.FabricanteVeiculoId
                            : (int?)null
                    ));


            CreateMap<Endereco, EnderecoViewModel>();
            CreateMap<Cliente, ClienteViewModel>();
            CreateMap<VendaListaDto, VendaViewModel>();
            CreateMap<Endereco, EnderecoViewModel>();
            CreateMap<VendaListaDto, VendaListaViewModel>();

        }
    }
}