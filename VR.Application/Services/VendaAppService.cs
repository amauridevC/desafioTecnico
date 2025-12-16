using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VR.Application.DTOs;
using VR.Application.Interfaces;
using VR.Domain.Entities;
using VR.Domain.Interfaces;

namespace VR.Application.Services
{
    public class VendaAppService : AppServiceBase<Venda>, IVendaAppService
    {
        private readonly IVendaRepository _vendaRepository;
        private readonly IVeiculoRepository _veiculoRepository;

        public VendaAppService(IVendaRepository vendaRepository, IVeiculoRepository veiculoRepository, IUnitOfWork unitOfWork)
            : base(vendaRepository, unitOfWork)
        {
            _vendaRepository = vendaRepository;
            _veiculoRepository = veiculoRepository;
        }


        public bool PrecoVendaValido(Venda venda)
        {
            var veiculo = _veiculoRepository.GetById(venda.VeiculoId);

            if (veiculo == null || veiculo.IsDeleted)
                return false;

            return venda.PrecoVenda >= veiculo.Preco;
        }

        private static readonly Random _random = new Random();

        private string GerarProtocoloUnico()
        {
            const string letras = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            char l1 = letras[_random.Next(26)];
            char l2 = letras[_random.Next(26)];
            char l3 = letras[_random.Next(26)];

            return $"VEN-{DateTime.Now:yyyyMMdd-HHmmss}{l1}{l2}{l3}";
        }

        public override void Update(Venda venda)
        {
  
            if (venda.DataVenda.Date > DateTime.Today)
                throw new InvalidOperationException("DATA_VENDA_NO_FUTURO");

            if (!PrecoVendaValido(venda))
                throw new InvalidOperationException("PRECO_VENDA_INVALIDO");

            base.Update(venda);
        }

        public override void Add(Venda venda)
        {
            if (venda.DataVenda.Date > DateTime.Today)
                throw new InvalidOperationException("DATA_VENDA_INVALIDA");

            if (!PrecoVendaValido(venda))
                throw new InvalidOperationException("PRECO_VENDA_INVALIDO");

            venda.Protocolo = GerarProtocoloUnico();

            base.Add(venda);
        }


        public IEnumerable<VendaListaDto> ObterTodasParaListagem()
        {
            var vendas = _vendaRepository.ObterTodasComRelacionamento();

            return vendas.Select(v => new VendaListaDto
            {
                Id = v.Id,
                Protocolo = v.Protocolo,
                DataVenda = v.DataVenda,
                PrecoVenda = v.PrecoVenda,
                NomeCliente = v.Cliente != null && !v.Cliente.IsDeleted ? v.Cliente.Nome : "Cliente excluído",
                CpfCliente = v.Cliente != null && !v.Cliente.IsDeleted ? v.Cliente.Cpf : "N/A",
                TelefoneCliente = v.Cliente != null && !v.Cliente.IsDeleted ? v.Cliente.Telefone : "N/A",
                ModeloVeiculo = v.Veiculo != null && !v.Veiculo.IsDeleted ? v.Veiculo.Modelo : "Veículo excluído",
                NomeFabricante = v.Veiculo?.Fabricante != null && !v.Veiculo.Fabricante.IsDeleted
                       ? v.Veiculo.Fabricante.Nome : "Fabricante excluído"
            }).ToList();
        }

        public RelatorioVendasMensalDto ObterRelatorioMensal(int? mes = null, int? ano = null)
        {
            if (!mes.HasValue) mes = DateTime.Today.Month;
            if (!ano.HasValue) ano = DateTime.Today.Year;

            var vendasDoMes = ObterTodasParaListagem()
                .Where(v => v.DataVenda.Month == mes && v.DataVenda.Year == ano)
                .ToList();

            var totalVendido = vendasDoMes.Sum(v => v.PrecoVenda);

            return new RelatorioVendasMensalDto
            {
                Mes = mes.Value,
                Ano = ano.Value,
                TotalVendido = totalVendido,
                TotalVendas = vendasDoMes.Count,

                PorFabricante = vendasDoMes
                    .GroupBy(v => v.NomeFabricante)
                    .Select(g => new FabricanteRelatorioDto
                    {
                        Fabricante = g.Key,
                        Quantidade = g.Count(),
                        TotalVendido = g.Sum(v => v.PrecoVenda),
                        TotalGeral = totalVendido
                    })
                    .OrderByDescending(x => x.TotalVendido)
                    .ToList(),

                PorTipoVeiculo = vendasDoMes
                    .GroupBy(v => v.TipoVeiculo.ToString())
                    .Select(g => new TipoVeiculoRelatorioDto
                    {
                        Tipo = g.Key,
                        Quantidade = g.Count(),
                        TotalVendido = g.Sum(v => v.PrecoVenda),
                        TotalGeral = totalVendido
                    })
                    .OrderByDescending(x => x.TotalVendido)
                    .ToList()
            };
        }

     
          public Venda ObterPorId(int id)
        {
            return _vendaRepository.ObterPorIdComRelacionamentos(id);
        }

    
}
}
