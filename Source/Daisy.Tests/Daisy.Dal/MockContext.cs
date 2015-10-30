using System;
using System.Collections.Generic;

using Daisy.Dal.Context;
using Daisy.Dal.Context.Interfaces;
using Daisy.Dal.Domain;


namespace Daisy.Tests.Daisy.Dal
{
    public class MockContext : IContext
    {
        public MockContext()
        {
            Storage = new List<object>();
        }


        public List<object> Storage { get; set; }


        public void AddOrUpdate<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            Storage.Add(entity);
        }


        public List<TEntity> GetAll<TEntity>() where TEntity : BaseEntity
        {
            throw new NotImplementedException();
        }


        public TEntity GetById<TEntity>(Guid id) where TEntity : BaseEntity
        {
            throw new NotImplementedException();
        }


        public void Remove<TEntity>(Guid id) where TEntity : BaseEntity
        {
            throw new NotImplementedException();
        }
    }
}
