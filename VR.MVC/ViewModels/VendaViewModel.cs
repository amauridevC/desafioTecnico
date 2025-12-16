using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VR.Domain.Entities;

namespace VR.MVC.ViewModels
{
    public class VendaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Selecione um fabricante")]
        [Display(Name = "Fabricante")]
        public int FabricanteVeiculoId { get; set; }
        public FabricanteVeiculo Fabricante { get; set; }

        [Required(ErrorMessage = "Selecione um Veículo")]
        [Display(Name = "Veículo")]
        public int VeiculoId { get; set; }
        public Veiculo Veiculo { get; set; }

        [Required(ErrorMessage = "Selecione um Cliente")]
        public int ClienteId { get; set; }
        public Cliente  Cliente { get; set; }

        [Display(Name = "Data da Venda")]
        [DataType(DataType.Date)]
        public DateTime DataVenda { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Informe o preço de venda")]
        [Display(Name = "Preço de Venda")]
        [DataType(DataType.Currency)]
        public decimal? PrecoVenda { get; set; }

        [Display(Name = "Protocolo")]
        public string Protocolo { get; set; }
    }
}