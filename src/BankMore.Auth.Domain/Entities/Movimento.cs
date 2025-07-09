namespace BankMore.Auth.Domain.Entities
{
    public class Movimento
    {
        public Guid Id { get; }
        public Guid IdContaCorrente { get; }
        public DateTime DataMovimento { get; }
        public char Tipo { get; }
        public decimal Valor { get; }

        public Movimento(Guid id, Guid idContaCorrente, DateTime dataMovimento, char tipo, decimal valor)
        {
            Id = id;
            IdContaCorrente = idContaCorrente;
            DataMovimento = dataMovimento;
            Tipo = tipo;
            Valor = valor;
        }
    }
}
 
