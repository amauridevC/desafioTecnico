using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using VR.Domain.Entities;
using VR.Domain.Interfaces;
using VR.Infra.Data.Context;

namespace VR.Infra.Data.Repositories
{
    public class VendaRepository : RepositoryBase<Venda>, IVendaRepository
    {
        public VendaRepository(VRContext context) : base(context)
        {
        }

        public Venda ObterPorIdComRelacionamentos(int id)
        {
            return _dbSet
                .Include(v => v.Cliente)
                .Include(v => v.Veiculo)
                .Include(v => v.Veiculo.Fabricante)
                .FirstOrDefault(v => v.Id == id);
        }


        public IEnumerable<Venda> ObterTodasComRelacionamento()
        {
            return _dbSet
            .Include(v => v.Cliente)
            .Include(v => v.Veiculo)
            .Include(v => v.Veiculo.Fabricante)
            .ToList();
        }
   
    }
}
