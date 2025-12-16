using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VR.Domain.Entities;
using VR.Domain.Interfaces;
using VR.Infra.Data.Context;

namespace VR.Infra.Data.Repositories
{
    public class ClienteRepository : RepositoryBase<Cliente>, IClienteRepository
    {
        public ClienteRepository(VRContext context) : base(context)
        {

        }
    }
}
