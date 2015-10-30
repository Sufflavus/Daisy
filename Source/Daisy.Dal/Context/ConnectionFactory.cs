using System;
using System.Data.SqlClient;

using Daisy.Dal.Context.Interfaces;


namespace Daisy.Dal.Context
{
    public class ConnectionFactory : IConnectionFactory
    {
        public IConnection CreateDapperConnection()
        {
            var connection = new SqlConnection(SettingsProvider.GetDbConnectionString());
            return new DapperConnection(connection);
        }
    }
}
