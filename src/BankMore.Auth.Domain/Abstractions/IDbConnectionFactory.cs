using System.Data;

namespace BankMore.Auth.Domain.Abstractions
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
