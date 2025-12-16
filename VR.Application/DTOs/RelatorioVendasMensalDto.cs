// VR.Application/DTOs/RelatorioVendasMensalDto.cs
using System.Collections.Generic;

namespace VR.Application.DTOs
{
    public class RelatorioVendasMensalDto
    {
        public int Mes { get; set; }
        public int Ano { get; set; }
        public decimal TotalVendido { get; set; }
        public int TotalVendas { get; set; }

        public List<FabricanteRelatorioDto> PorFabricante { get; set; } = new List<FabricanteRelatorioDto>();
        public List<TipoVeiculoRelatorioDto> PorTipoVeiculo { get; set; } = new List<TipoVeiculoRelatorioDto>();
    }

    public class FabricanteRelatorioDto
    {
        public string Fabricante { get; set; } = "";
        public int Quantidade { get; set; }
        public decimal TotalVendido { get; set; }
        public decimal TotalGeral { get; set; }

    }

    public class TipoVeiculoRelatorioDto
    {
        public int Id { get; set; }
        public string Tipo { get; set; } = "";
        public int Quantidade { get; set; }
        public decimal TotalVendido { get; set; }
        public decimal TotalGeral { get; set; }
    }

    public class VeiculoRelatorioDto
    {
        public int Id { get; set; }
        public string Modelo { get; set; } = "";
        public string Fabricante { get; set; } = "";
        public string Tipo { get; set; } = "";
        public int Quantidade { get; set; }
        public decimal TotalVendido { get; set; }
    }
}