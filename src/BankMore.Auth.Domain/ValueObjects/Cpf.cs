namespace BankMore.Auth.Domain.ValueObjects
{
    public class CPF
    {
        public string Numero { get; private set; }
        public CPF(string numero)
        {
            if (string.IsNullOrWhiteSpace(numero))
                throw new ArgumentException("CPF é obrigatório", nameof(numero));

            numero = new string(numero.Where(char.IsDigit).ToArray());
            if (numero.Length !=11 ||  !ValidarCpf(numero))
                throw new ArgumentException("CPF inválido", nameof(numero));
            Numero = numero;
        }
        private bool ValidarCpf(string cpf)
        {
            if (cpf.Distinct().Count() == 1)
                return false;

            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf[..9];
            int soma = tempCpf
                .Select((t, i) => int.Parse(t.ToString()) * multiplicador1[i])
                .Sum();

            int resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;
            string digito = resto.ToString();

            tempCpf += digito;
            soma = tempCpf
                .Select((t, i) => int.Parse(t.ToString()) * multiplicador2[i])
                .Sum();

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;
            digito += resto;

            return cpf.EndsWith(digito);
        }
        public override bool Equals(object? obj)
        {
            return obj is CPF other && Numero == other.Numero;
        }

        public override int GetHashCode() => Numero.GetHashCode();

        public override string ToString() => Numero;
    }
}
