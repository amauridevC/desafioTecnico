using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VR.Application.Interfaces;
using VR.Domain.Entities;
using VR.Domain.Interfaces;
using VR.Domain.Utils;

namespace VR.Application.Services
{
    public class ClienteAppService : AppServiceBase<Cliente>, IClienteAppService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteAppService(IClienteRepository clienteRepository, IUnitOfWork unitOfWork)
          : base(clienteRepository, unitOfWork)
        {
            _clienteRepository = clienteRepository;
        }


        public bool CpfJaExiste(string cpf, int? idIgnorar = null)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            return _clienteRepository
                .GetAll()
                .Any(c => c.Cpf == cpf && c.Id != idIgnorar && !c.IsDeleted);
        }

        public override void Add(Cliente cliente)
        {
            cliente.Cpf = new string(cliente.Cpf.Where(char.IsDigit).ToArray());


            if (!ValidadorCpf.IsValid(cliente.Cpf))
                throw new InvalidOperationException("CPF inválido.");

            if (CpfJaExiste(cliente.Cpf))
                throw new InvalidOperationException("Já existe um cliente cadastrado com este CPF.");

            base.Add(cliente);
        }

        public override void Update(Cliente cliente)
        {
            cliente.Cpf = new string(cliente.Cpf.Where(char.IsDigit).ToArray());

            if (!ValidadorCpf.IsValid(cliente.Cpf))
                throw new InvalidOperationException("CPF inválido.");

            if (CpfJaExiste(cliente.Cpf, cliente.Id))
                throw new InvalidOperationException("Já existe um cliente cadastrado com este CPF.");
            base.Update(cliente);
        }
    }
}
