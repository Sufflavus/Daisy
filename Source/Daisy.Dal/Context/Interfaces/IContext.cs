using System;
using System.Collections.Generic;

using Daisy.Dal.Domain;


namespace Daisy.Dal.Context.Interfaces
{
    public interface IContext
    {
        void AddOrUpdate<TEntity>(TEntity entity) where TEntity : BaseEntity;
        List<TEntity> GetAll<TEntity>() where TEntity : BaseEntity;
        TEntity GetById<TEntity>(Guid id) where TEntity : BaseEntity;
        void Remove<TEntity>(Guid id) where TEntity : BaseEntity;
    }
}
