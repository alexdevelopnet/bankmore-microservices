using System.Text.RegularExpressions;
 
namespace BankMore.Auth.Domain.ValueObjects
{
    public class Email : IEquatable<Email>
    {
        private static readonly Regex EmailRegex =
            new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

        public string Endereco { get; }

        public Email(string endereco)
        {
            if (string.IsNullOrWhiteSpace(endereco))
                throw new ArgumentException("Email não pode ser vazio.", nameof(endereco));

            if (!EmailRegex.IsMatch(endereco))
                throw new ArgumentException("Formato de email inválido.", nameof(endereco));

            Endereco = endereco;
        }

        public override string ToString() => Endereco;

        public bool Equals(Email? other) => other is not null && Endereco == other.Endereco;

        public override bool Equals(object? obj) => Equals(obj as Email);

        public override int GetHashCode() => Endereco.GetHashCode();

        public static implicit operator string(Email email) => email.Endereco;

        public static explicit operator Email(string endereco) => new Email(endereco);

    }
}