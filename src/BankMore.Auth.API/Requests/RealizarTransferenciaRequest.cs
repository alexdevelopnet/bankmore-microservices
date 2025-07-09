namespace BankMore.Auth.API.Requests
{
    public class RealizarTransferenciaRequest
    {
        public Guid IdContaOrigem { get; set; }
        public Guid IdContaDestino { get; set; }
        public decimal Valor { get; set; }
        public string ChaveIdempotencia { get; set; } = default!;
    }
}
