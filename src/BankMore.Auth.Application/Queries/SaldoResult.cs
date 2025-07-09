namespace BankMore.Auth.Application.Queries
{
    public class SaldoResult
    {
        public int Numero { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateTime DataHora { get; set; }
        public decimal Saldo { get; set; }
    }
}
