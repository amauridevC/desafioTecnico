using System;
using System.Collections.Generic;
using VR.Domain.Entities;

namespace VR.Domain.Interfaces
{
    public interface IFabricanteVeiculoRepository : IRepository<FabricanteVeiculo>
    {

        IEnumerable<FabricanteVeiculo> GetAllComEndereco();
        FabricanteVeiculo GetByIdComEndereco(int id);
    }
}
