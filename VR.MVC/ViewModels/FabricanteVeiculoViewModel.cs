using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VR.Domain.Entities;

namespace VR.MVC.ViewModels
{
    public class FabricanteVeiculoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Preencha o campo Nome")]
        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Preencha o campo País")]
        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
        [Display(Name = "País")]
        public string Pais { get; set; }
        

        [Required(ErrorMessage = "Preencha o campo Telefone")]
        [MinLength(11, ErrorMessage = "O campo telefone deve conter exatamente 11 dígitos.")]
        [MaxLength(11, ErrorMessage = "O campo telefone deve conter exatamente 11 dígitos.")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Preencha o campo E-mail")]
        [EmailAddress(ErrorMessage = "Preencha com um E-mail válido")]
        [DisplayName("E-mail")]
        [MaxLength(120, ErrorMessage = "O E-mail deve conter no máximo 120 caracetres")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Preencha o campo Ano de Fundação")]
        [Display(Name = "Ano de Fundação")]
        [DataType(DataType.Date)]
        public DateTime AnoFundacao { get; set; }

        [Required(ErrorMessage = "Preencha o campo Website")]
        [MaxLength(150, ErrorMessage = "Máximo 150 caracteres")]
        [Display(Name = "Website")]
        [Url(ErrorMessage = "Preencha com uma URL válida")]
        public string Website { get; set; }
        public virtual ICollection<Veiculo> Veiculos { get; set; }

        public EnderecoViewModel Endereco { get; set; }


    }
}