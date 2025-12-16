using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VR.MVC.ViewModels
{
    public class ClienteViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Preencha o campo Nome")]
        [MaxLength(100, ErrorMessage = "O campo Nome deve conter no máximo 100 caracteres")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "O Nome deve conter apenas letras e espaços.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Preencha o campo CPF")]
        [MinLength(11, ErrorMessage = "O campo CPF deve conter exatamente 11 dígitos.")]
        [MaxLength(11, ErrorMessage = "O campo CPF deve conter exatamente 11 dígitos.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "O CPF deve conter apenas números.")]
        [Display(Name = "CPF")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Preencha o campo Telefone")]
        [MinLength(11, ErrorMessage = "O campo telefone deve conter exatamente 11 dígitos.")]
        [MaxLength(11, ErrorMessage = "O campo telefone deve conter exatamente 11 dígitos.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "O Telefone deve conter apenas números.")]
        public string Telefone { get; set; }

    }
}