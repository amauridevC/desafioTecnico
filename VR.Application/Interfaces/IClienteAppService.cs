using System;
using VR.Domain.Entities;

namespace VR.Application.Interfaces
{
    public interface IClienteAppService : IAppServiceBase<Cliente>
    {
        bool CpfJaExiste(string cpf, int? idIgnorar = null);
    }
}
