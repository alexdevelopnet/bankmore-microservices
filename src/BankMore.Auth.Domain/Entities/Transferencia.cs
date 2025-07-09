namespace BankMore.Auth.Domain.Entities
{
    public class Transferencia
    {
        public Guid Id { get; }
        public Guid IdContaOrigem { get; }
        public Guid IdContaDestino { get; }
        public DateTime DataMovimento { get; }
        public decimal Valor { get; }

        public Transferencia(Guid id, Guid idContaOrigem, Guid idContaDestino, DateTime dataMovimento, decimal valor)
        {
            Id = id;
            IdContaOrigem = idContaOrigem;
            IdContaDestino = idContaDestino;
            DataMovimento = dataMovimento;
            Valor = valor;
        }
    }
}
