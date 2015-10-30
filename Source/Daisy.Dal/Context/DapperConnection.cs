using System;
using System.Collections.Generic;
using System.Data.Common;

using Daisy.Dal.Context.Interfaces;
using Daisy.Dal.Domain;

using Dapper;


namespace Daisy.Dal.Context
{
    public class DapperConnection : IConnection
    {
        private readonly DbConnection _connection;


        public DapperConnection(DbConnection connection)
        {
            _connection = connection;
        }


        public void Close()
        {
            _connection.Close();
        }


        public void Open()
        {
            _connection.Open();
        }


        public IEnumerable<TEntity> Query<TEntity>(string query) where TEntity : BaseEntity
        {
            try
            {
                Open();
                return _connection.Query<TEntity>(query);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Close();
            }
        }


        public SqlMapper.GridReader QueryMultiple(string query, object param = null)
        {
            return _connection.QueryMultiple(query, param);
        }


        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Close();
                _connection.Dispose();
            }
        }
    }
}
