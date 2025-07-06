using BankMore.Auth.Domain.ValueObjects;

namespace BankMore.Auth.Domain.Entities
{
    public class Usuario
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; } = string.Empty;  
        public CPF Cpf { get; private set; } = default!;
        public Email Email { get; private set; } = default!;
        public string SenhaHash { get; private set; } = string.Empty;
        public bool Ativo { get; private set; }
        public DateTime CriadoEm { get; private set; }

        public Usuario(Guid id, string nome, CPF cpf, Email email, string senhaHash)
        {
            Id = id;
            Nome = nome;
            Cpf = cpf;
            Email = email;
            SenhaHash = senhaHash;
            Ativo = true;
            CriadoEm = DateTime.UtcNow;
        }

        public static Usuario Criar(string nome, CPF cpf, Email email, string senhaHash)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome é obrigatório");

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email é obrigatório");

            if (string.IsNullOrWhiteSpace(senhaHash))
                throw new ArgumentException("Senha é obrigatória");

            return new Usuario(Guid.NewGuid(), nome, cpf, email, senhaHash);
        }

        public void Inativar() => Ativo = false;
    }
}
