using MediatR;
namespace BankMore.Auth.Application.Commands
{
        public sealed record CriarUsuarioCommand(string Nome, string Cpf,string Email, string Senha) : IRequest<Guid>;
}