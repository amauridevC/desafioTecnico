using System;
using System.Collections.Generic;
using VR.Domain.Entities;

namespace VR.Application.Interfaces
{
    public interface IFabricanteVeiculoAppService : IAppServiceBase<FabricanteVeiculo>
    {
        IEnumerable<FabricanteVeiculo> GetAllComEndereco();
        FabricanteVeiculo GetByIdComEndereco(int id);
        bool AnoFundacaoInvalido(DateTime anoFundacao);
    }
}
