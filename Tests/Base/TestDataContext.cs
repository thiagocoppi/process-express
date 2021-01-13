using Infraestrutura.Context;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Tests.Base
{
    public sealed class TestDataContext : IProcessExpressContext
    {
        private readonly IConfiguration _configuration;
        private IDbConnection _dbConnection;
        private bool _disposed = false;

        public TestDataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            if (_dbConnection?.State == ConnectionState.Open)
            {
                _dbConnection.Close();
            }

            _disposed = true;
        }

        /// <summary>
        /// Retorna uma nova instância de conexão ou reutiliza a mesma caso já tenha aberto.
        /// </summary>
        public IDbConnection GetConnection()
        {
            if (_dbConnection is null)
            {
                _dbConnection = new SqliteConnection(_configuration.GetConnectionString("DefaultConnection"));
            }

            return _dbConnection;
        }
    }
}