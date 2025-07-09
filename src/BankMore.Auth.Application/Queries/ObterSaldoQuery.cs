

using MediatR;

namespace BankMore.Auth.Application.Queries
{
    public class ObterSaldoQuery : IRequest<decimal>
    {
        public Guid Id { get; }

        public ObterSaldoQuery(Guid id)
        {
            Id = id;
        }
    }
}