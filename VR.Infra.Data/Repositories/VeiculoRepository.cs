

using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using VR.Domain.Entities;
using VR.Domain.Interfaces;
using VR.Infra.Data.Context;

namespace VR.Infra.Data.Repositories
{
    public class VeiculoRepository : RepositoryBase<Veiculo>, IVeiculoRepository
    {
        public VeiculoRepository (VRContext context) : base(context)
        {
        }

        public IEnumerable<Veiculo> GetAllComFabricante()
        {
            return _dbSet
                   .Include(v => v.Fabricante)  
                   .ToList();
        }

        public Veiculo GetByIdComFabricante(int id)
        {
            return _dbSet
                   .Include(v => v.Fabricante)
                   .FirstOrDefault(v => v.Id == id);
        }

        public IEnumerable<Veiculo> ObterVeiculosPorModelo(string modelo)
        {
            if (string.IsNullOrWhiteSpace(modelo))
                return Enumerable.Empty<Veiculo>();

            modelo = modelo.Trim();

            return _dbSet
                .AsNoTracking()                           
                .Where(v => v.Modelo.Contains(modelo))   
                .OrderBy(v => v.Modelo)                   
                .ToList();
        }
    }
}
