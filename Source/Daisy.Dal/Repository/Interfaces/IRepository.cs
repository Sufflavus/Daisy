using System;
using System.Collections.Generic;

using Daisy.Dal.Domain;


namespace Daisy.Dal.Repository.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity : BaseEntity
    {
        void AddOrUpdate(TEntity entity);
        List<TEntity> GetAll();
        TEntity GetById(Guid id);
        void Remove(Guid id);
    }
}
