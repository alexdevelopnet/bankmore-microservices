using BankMore.Auth.Domain.Entities;
using BankMore.Auth.Domain.ValueObjects;

namespace BankMore.Auth.Infrastructure.Mappers
{
    public class UsuarioDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;

        public Usuario ToEntity()
        {
            var cpfValueObject = new CPF(Cpf);
            var emailValueObject = new Email(Email);
            var senhaHash = Senha;

            return Usuario.Criar(Nome, cpfValueObject, emailValueObject, senhaHash);
        }
    }
}