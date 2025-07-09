using MediatR;

namespace BankMore.Auth.Application.Queries
{
    public record GetSaldoQuery : IRequest<SaldoResult>;
}
