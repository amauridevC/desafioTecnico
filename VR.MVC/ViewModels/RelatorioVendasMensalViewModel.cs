using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VR.MVC.ViewModels
{
    public class RelatorioVendasMensalViewModel
    {
        public int Mes { get; set; }
        public int Ano { get; set; }
        public decimal TotalVendido { get; set; }
        public int TotalVendas { get; set; }

        public List<FabricanteRelatorio> PorFabricante { get; set; } = new List<FabricanteRelatorio>();

        public List<TipoVeiculoRelatorio> PorTipoVeiculo { get; set; } = new List<TipoVeiculoRelatorio>();
    }

    public class FabricanteRelatorio
    {
        public string Fabricante { get; set; }
        public int Quantidade { get; set; }
        public decimal TotalVendido { get; set; }
        public decimal Percentual => TotalVendido > 0 ? (TotalVendido / TotalGeral) * 100 : 0;
        public decimal TotalGeral { get; set; } 
    }

    public class TipoVeiculoRelatorio
    {
        public string Tipo { get; set; } = "";
        public int Quantidade { get; set; }
        public decimal TotalVendido { get; set; }
        public decimal Percentual => TotalVendido > 0 ? (TotalVendido / TotalGeral) * 100 : 0;
        public decimal TotalGeral { get; set; }
    }

}