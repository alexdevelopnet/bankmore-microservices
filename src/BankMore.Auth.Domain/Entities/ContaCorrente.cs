namespace BankMore.Auth.Domain.Entities
{
    
        public class ContaCorrente
        {
            public Guid Id { get; private set; }
            public int Numero { get; private set; }
            public string Nome { get; private set; }
            public bool Ativo { get; private set; }
            public string Senha { get; private set; }
            public string Salt { get; private set; }

            public ContaCorrente(Guid id, int numero, string nome, string senhaHash, string salt)
            {
                Id = id;
                Numero = numero;
                Nome = nome;
                Senha = senhaHash;
                Salt = salt;
                Ativo = true;
            }

            public void Desativar() => Ativo = false;
        }
    }
 