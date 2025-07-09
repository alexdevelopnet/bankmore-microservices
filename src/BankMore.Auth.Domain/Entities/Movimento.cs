using System;

namespace BankMore.Auth.Domain.Entities
{
    public class Movimento
    {
        public Guid Id { get; private set; }
        public Guid IdContaCorrente { get; private set; }
        public DateTime DataMovimento { get; private set; }
        public string TipoMovimento { get; private set; } = string.Empty; // "C" ou "D"
        public decimal Valor { get; private set; }
        public string ChaveIdempotencia { get; private set; } = string.Empty;

        public Movimento(Guid id, Guid idContaCorrente, DateTime dataMovimento, string tipoMovimento, decimal valor, string chaveIdempotencia)
        {
            Id = id;
            IdContaCorrente = idContaCorrente;
            DataMovimento = dataMovimento;
            TipoMovimento = tipoMovimento;
            Valor = valor;
            ChaveIdempotencia = chaveIdempotencia;
        }
 
        public Movimento() { }
    }
}
