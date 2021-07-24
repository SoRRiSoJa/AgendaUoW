using AgendaUoW.Middlewares;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AgendaUoW.Persistence.Config
{
    public sealed class DbSession : IDisposable
    {
        private readonly IConfiguration _configuration;
        public IDbConnection Connection { get; set; }
        public IDbTransaction Transaction { get; set; }
        public DbSession(IConfiguration _configuration)
        {
            this._configuration = _configuration ?? throw new ArgumentNullException(nameof(_configuration));
            try
            {
                Connection = new SqlConnection(_configuration.GetConnectionString("AgendaWS"));
                Connection.Open();
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(500, $"Erro ao realizar conexão:{ex.Message}");
            }

        }
        public void Dispose() => Connection?.Dispose();

    }
}
