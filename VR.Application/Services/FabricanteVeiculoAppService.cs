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
    public class FabricanteVeiculoAppService : AppServiceBase<FabricanteVeiculo>, IFabricanteVeiculoAppService
    {
        private readonly IFabricanteVeiculoRepository _fabricanteVeiculoRepository;
        public FabricanteVeiculoAppService(IFabricanteVeiculoRepository fabricanteVeiculoRepository, IUnitOfWork unitOfWork)
            : base(fabricanteVeiculoRepository, unitOfWork)
        {
            _fabricanteVeiculoRepository = fabricanteVeiculoRepository;
        }

        public IEnumerable<FabricanteVeiculo> GetAllComEndereco()
        {
            return _fabricanteVeiculoRepository.GetAllComEndereco();
        }

        public FabricanteVeiculo GetByIdComEndereco(int id)
        {
            return _fabricanteVeiculoRepository.GetByIdComEndereco(id);
        }
        public bool AnoFundacaoInvalido(DateTime anoFundacao)
        {
            return anoFundacao.Year > DateTime.Now.Year;
        }

    }
}
