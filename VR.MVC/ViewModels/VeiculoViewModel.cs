using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VR.Domain.Entities;
using VR.Domain.Enums;

namespace VR.MVC.ViewModels
{
    public class VeiculoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Preencha o campo Modelo")]
        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "Preencha o campo Ano de Fabricação")]
        [Display(Name = "Ano de Fabricação")]
        [DataType(DataType.Date)]
        public DateTime AnoFabricacao { get; set; }

        [Required(ErrorMessage = "Preencha o campo Preço")]
        [DataType(DataType.Currency)]
        [Display(Name = "Preço")]
        [Range(1, 10_000_000.00, ErrorMessage = "O preço deve ser entre R$ 1 e R$ 10.000.000,00")]
        public Decimal? Preco { get; set; }

        [Required(ErrorMessage = "Preencha o campo Tipo de Veículo")]
        [Display(Name = "Tipo de Veículo")]
        public TipoVeiculo TipoDeVeiculo { get; set; }

        [MaxLength(500, ErrorMessage = "Máximo 500 caracteres")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Selecione um fabricante")]
        [Display(Name = "Fabricante")]
        public int FabricanteVeiculoId { get; set; }
        public FabricanteVeiculo Fabricante { get; set; }

    }
}