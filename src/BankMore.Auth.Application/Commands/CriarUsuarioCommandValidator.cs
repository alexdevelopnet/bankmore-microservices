using BankMore.Auth.Domain.ValueObjects;
using FluentValidation;

namespace BankMore.Auth.Application.Commands
{
    public sealed class CriarUsuarioCommandValidator : AbstractValidator<CriarUsuarioCommand>
    {
        public CriarUsuarioCommandValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome é obrigatório")
                .MinimumLength(3).WithMessage("Nome deve ter pelo menos 3 caracteres");

            RuleFor(x => x.Cpf)
                .NotEmpty().WithMessage("CPF é obrigatório")
                .Must(ValidarCPF).WithMessage("CPF inválido");

            RuleFor(x => x.Senha)
                .NotEmpty().WithMessage("Senha é obrigatória")
                .MinimumLength(6).WithMessage("Senha deve ter pelo menos 6 caracteres");
        }

        private bool ValidarCPF(string cpf)
        {
            return CPF.ValidarFormato(cpf);  
        }
    }
}