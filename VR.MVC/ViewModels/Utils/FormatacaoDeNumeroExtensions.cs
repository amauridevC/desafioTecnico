using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VR.MVC.ViewModels.Utils
{
    public static class FormatacaoDeNumeroExtensions
    {
        public static string FormatarTelefone(this string telefone)
        {
            if (string.IsNullOrEmpty(telefone))
            {
                return string.Empty;
            }

            string apenasDigitos = new string(telefone.Where(char.IsDigit).ToArray());

            if (apenasDigitos.Length == 11)
            {
                return $"({apenasDigitos.Substring(0, 2)}) {apenasDigitos.Substring(2, 5)}-{apenasDigitos.Substring(7, 4)}";
            }
            else if (apenasDigitos.Length == 10)
            {
                return $"({apenasDigitos.Substring(0, 2)}) {apenasDigitos.Substring(2, 4)}-{apenasDigitos.Substring(6, 4)}";
            }

            return telefone;
        }

        public static string FormatarCpf(this string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
            {
                return string.Empty;
            }

            string apenasDigitos = new string(cpf.Where(char.IsDigit).ToArray());

            if (apenasDigitos.Length == 11)
            {
                return $"{apenasDigitos.Substring(0, 3)}.{apenasDigitos.Substring(3, 3)}.{apenasDigitos.Substring(6, 3)}-{apenasDigitos.Substring(9, 2)}";
            }

            return cpf;
        }
    }
}