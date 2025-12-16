using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VR.MVC.ViewModels
{
    public class VendaListaViewModel
    {
        public int Id { get; set; }
        public string Protocolo { get; set; }

        [Display(Name = "Data da venda")]
        public DateTime DataVenda { get; set; }

        [Display(Name = "Preço de venda")]
        [DataType(DataType.Currency)]
        public decimal PrecoVenda { get; set; }

        [Display(Name = "Nome do cliente")]
        public string NomeCliente { get; set; }

        [Display(Name = "CPF")]
        public string CpfCliente { get; set; }

        [Display(Name = "Telefone")]
        public string TelefoneCliente { get; set; }

        [Display(Name = "Modelo do veículo")]
        public string ModeloVeiculo { get; set; }

        [Display(Name = "Fabricante")]
        public string NomeFabricante { get; set; }
    }
}