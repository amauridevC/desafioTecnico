using System;
using System.Linq;

namespace VR.Domain.Utils
{
    public static class ValidadorCpf
    {
       
        public static bool IsValid(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            string cpfLimpo = new string(cpf.Where(char.IsDigit).ToArray());

  
            if (cpfLimpo.Length != 11)
                return false;

            if (IsSequencial(cpfLimpo))
                return false;

            // Calcula os dígitos verificadores
            int[] multiplicadores1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicadores2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpfLimpo.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicadores1[i];

            int resto = soma % 11;
            int digito1 = resto < 2 ? 0 : 11 - resto;

            tempCpf += digito1;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicadores2[i];

            resto = soma % 11;
            int digito2 = resto < 2 ? 0 : 11 - resto;

            return cpfLimpo.EndsWith(digito1.ToString() + digito2.ToString());
        }

        private static bool IsSequencial(string cpf)
        {
            return cpf.Distinct().Count() == 1;
        }
    }
}