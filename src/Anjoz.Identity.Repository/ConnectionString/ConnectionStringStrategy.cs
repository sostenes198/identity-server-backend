using Anjoz.Identity.Repository.Contratos;
using Microsoft.Extensions.Configuration;

namespace Anjoz.Identity.Repository.ConnectionString
{
    public class ConnectionStringStrategy : IConnectionStringStrategy
    {
        private readonly IConfiguration _configuracao;

        private const string Connection = "DefaultConnection";

        public ConnectionStringStrategy(IConfiguration configuracao)
        {
            _configuracao = configuracao;
        }

        public string ObterConnectionString()
        {
            return _configuracao.GetConnectionString(Connection);
        }
    }
}