using System;
using System.Collections.Generic;

using Daisy.Dal.Context;
using Daisy.Dal.Domain;
using Daisy.Dal.Repository.Interfaces;


namespace Daisy.Dal.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected Repository(IContext context)
        {
            _context = context;
        }


        private IContext _context { get; set; }


        public void AddOrUpdate(TEntity entity)
        {
            _context.AddOrUpdate(entity);
        }


        public virtual List<TEntity> GetAll()
        {
            return _context.GetAll<TEntity>();
        }


        public virtual TEntity GetById(Guid id)
        {
            return _context.GetById<TEntity>(id);
        }


        public void Remove(Guid id)
        {
            _context.Remove<TEntity>(id);
        }
    }
}
