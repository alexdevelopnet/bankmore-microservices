using BankMore.Auth.Domain.ValueObjects;

namespace BankMore.Auth.Domain.Entities
{
    internal class Usuario
    {
        public Guid Id { get; private set; }
        public CPF Cpf { get; private set; }
        public string Nome { get; private set; }
        public string SenhaHash { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public bool Ativo { get; private set; }

        public Usuario()
        {
            
        }
        public Usuario(string nome, CPF cpf, string senhaHash)
        {
            Id = Guid.NewGuid();
            Nome = nome ?? throw new ArgumentNullException(nameof(nome));
            Cpf = cpf ?? throw new ArgumentNullException(nameof(cpf));
            SenhaHash = senhaHash ?? throw new ArgumentNullException(nameof(senhaHash));
            Ativo = true;
            DataCriacao = DateTime.UtcNow;
        }
        public static Usuario Criar(string nome, string cpf, string senhaHash)
        {
            return new Usuario(nome, new CPF(cpf), senhaHash);
        }

        public void Inativar()
        {
            if (!Ativo)
                throw new InvalidOperationException("Usuário já está inativo.");

            Ativo = false;
        }
    }
}
