using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace BankMore.Auth.Domain.ValueObjects
{
    public class CPF : IEquatable<CPF>
    {
        public string Numero { get; private set; }

        public CPF(string numero)
        {
            if (string.IsNullOrWhiteSpace(numero))
                throw new ArgumentException("CPF não pode ser vazio.");

            var cpfLimpo = Limpar(numero);

            if (!EhValido(cpfLimpo))
                throw new ArgumentException("CPF inválido.");

            Numero = cpfLimpo;
        }

        public string Formatado => Convert.ToUInt64(Numero).ToString(@"000\.000\.000\-00");

        private static string Limpar(string cpf)
        {
            return Regex.Replace(cpf, "[^0-9]", "");
        }

        private static bool EhValido(string cpf)
        {
            if (cpf.Length != 11 || cpf.Distinct().Count() == 1)
                return false;

            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            int digito1 = resto < 2 ? 0 : 11 - resto;

            tempCpf += digito1;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            int digito2 = resto < 2 ? 0 : 11 - resto;

            string cpfCalculado = tempCpf + digito2;

            return cpf == cpfCalculado;
        }

        public override string ToString() => Formatado;

        public override bool Equals(object? obj) => Equals(obj as CPF);

        public bool Equals(CPF? other) =>
            other is not null && Numero == other.Numero;

        public override int GetHashCode() => Numero.GetHashCode();

        // Método estático público para validar CPF (use no handler)
        public static bool ValidarFormato(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            var cpfLimpo = Limpar(cpf);
            return EhValido(cpfLimpo);
        }
    }
}
