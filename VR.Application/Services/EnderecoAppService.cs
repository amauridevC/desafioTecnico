using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VR.Application.Interfaces;
using VR.Domain.Entities;
using VR.Domain.Interfaces;

namespace VR.Application.Services
{
    public class EnderecoAppService : AppServiceBase<Endereco>, IEnderecoAppService
    {
        private readonly IEnderecoRepository _enderecoRepository;
        public EnderecoAppService(IEnderecoRepository enderecoRepository, IUnitOfWork unitOfWork)
            : base(enderecoRepository, unitOfWork)
        {
            _enderecoRepository = enderecoRepository;
        }

    }
}
