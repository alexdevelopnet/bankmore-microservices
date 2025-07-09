namespace BankMore.Auth.Domain.Entities
{
    public class ContaCorrenteEntity
    {
        public Guid Id { get; private set; }
        public int Numero { get; private set; }
        public string Nome { get; private set; }
        public bool Ativo { get; private set; }
        public string Senha { get; private set; }
        public string Salt { get; private set; }

        public ContaCorrenteEntity(Guid id, int numero, string nome, bool ativo, string senha, string salt)
        {
            Id = id;
            Numero = numero;
            Nome = nome;
            Ativo = ativo;
            Senha = senha;
            Salt = salt;
        }
    }
}
