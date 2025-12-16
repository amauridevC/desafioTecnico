using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VR.Domain.Entities;
using VR.Domain.Interfaces;
using VR.Infra.Data.Context;

namespace VR.Infra.Data.Repositories
{
    public class FabricanteVeiculoRepository : RepositoryBase<FabricanteVeiculo>, IFabricanteVeiculoRepository
    {
        public FabricanteVeiculoRepository(VRContext context) : base(context)
        {
        }

        public IEnumerable<FabricanteVeiculo> GetAllComEndereco()
        {
            return _dbSet
                   .Include(f => f.Endereco)
                   .ToList();
        }

        public FabricanteVeiculo GetByIdComEndereco(int id)
        {
            return _dbSet
                   .Include(f => f.Endereco)
                   .FirstOrDefault(f => f.Id == id);
        }

    }
}
