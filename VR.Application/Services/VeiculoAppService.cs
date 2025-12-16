using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VR.Application.Interfaces;
using VR.Domain.Entities;
using VR.Domain.Enums;
using VR.Domain.Interfaces;

namespace VR.Application.Services
{
    public class VeiculoAppService : AppServiceBase<Veiculo>, IVeiculoAppService
    {
        private readonly IVeiculoRepository _veiculoRepository ;

        public VeiculoAppService(IVeiculoRepository veiculoRepository, IUnitOfWork unitOfWork)
            : base(veiculoRepository, unitOfWork)
        {
            _veiculoRepository = veiculoRepository;
        }

        public override void Add(Veiculo veiculo)
        {
            ValidarPreco(veiculo.Preco);
            base.Add(veiculo);
        }

        public override void Update(Veiculo veiculo)
        {
            ValidarPreco(veiculo.Preco);
            base.Update(veiculo);
        }

        private void ValidarPreco(decimal preco)
        {
            if (preco <= 1)
                throw new InvalidOperationException("PRECO_ZERADO_OU_NEGATIVO");

            if (preco > 10_000_000)
                throw new InvalidOperationException("PRECO_MAXIMO");
        }

        public IEnumerable<Veiculo> BuscarPorFabricante(int fabricanteId)
        {
            return _veiculoRepository
                   .GetAllComFabricante()  
                   .Where(v => v.FabricanteVeiculoId == fabricanteId)
                   .OrderBy(v => v.Modelo);
        }

        public IEnumerable<Veiculo> GetAllComFabricante()
        {
            return _veiculoRepository.GetAllComFabricante();
        }

        public Veiculo GetByIdComFabricante(int id)
        {
            return _veiculoRepository.GetByIdComFabricante(id);
        }

        public IEnumerable<Veiculo> ObterVeiculosPorModelo(string modelo)
        {
            return _veiculoRepository.ObterVeiculosPorModelo(modelo);
        }

        public bool TipoVeiculoValido(TipoVeiculo tipo)
        {
            return Enum.IsDefined(typeof(TipoVeiculo), tipo);
        }
    }
}
