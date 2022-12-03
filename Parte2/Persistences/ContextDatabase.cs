using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Parte2.Persistences
{
    public class ContextDatabase
    {
        public ContextDatabase(){ }
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public ContextDatabase(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("cn");
        }
        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
