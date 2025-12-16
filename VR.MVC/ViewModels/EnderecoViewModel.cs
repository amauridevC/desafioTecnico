using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VR.Domain.Entities;

namespace VR.MVC.ViewModels
{
    public class EnderecoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Preencha o campo Rua")]
        [MaxLength(90, ErrorMessage = "A Rua deve conter no máximo 90 caracteres")]
        public string Rua { get; set; }

        [Required(ErrorMessage = "Preencha o campo Cidade")]
        [MaxLength(36, ErrorMessage = "A Cidade deve conter no máximo 36 caracteres")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "Preencha o campo Estado")]
        [MaxLength(20, ErrorMessage = "O Estado deve conter no máximo 20 caracteres")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "Preencha o campo Estado")]
        [MaxLength(8, ErrorMessage = "O CEP deve conter no máximo 8 caracteres")]
        public string Cep { get; set; }
        public  FabricanteVeiculo FabricanteVeiculo { get; set; }
        public int FabricanteVeiculoId { get; set; }

    }
}