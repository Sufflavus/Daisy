using System;
using System.Collections.Generic;

using Daisy.Dal.Domain;

using Dapper;


namespace Daisy.Dal.Context.Interfaces
{
    public interface IConnection : IDisposable
    {
        void Close();
        void Open();
        IEnumerable<TEntity> Query<TEntity>(string query) where TEntity : BaseEntity;
        SqlMapper.GridReader QueryMultiple(string query, object param = null);
    }
}
