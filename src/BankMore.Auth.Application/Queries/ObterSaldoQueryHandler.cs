using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankMore.Auth.Domain.Repositories;
using MediatR;

namespace BankMore.Auth.Application.Queries
{
    public class ObterSaldoQueryHandler: IRequestHandler<ObterSaldoQuery, decimal>
    {
        private readonly IContaCorrenteRepository _repository;

        public ObterSaldoQueryHandler(IContaCorrenteRepository repository)
        {
            _repository = repository;
        }

        public async Task<decimal> Handle(ObterSaldoQuery request, CancellationToken cancellationToken)
        {
            return await _repository.ObterSaldoAsync(request.Id);
        }
    }
}