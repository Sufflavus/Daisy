using System;
using System.Collections.Generic;
using System.Linq;

using Daisy.Dal.Context.Interfaces;
using Daisy.Dal.Domain;

using NHibernate;
using NHibernate.Linq;


namespace Daisy.Dal.Context
{
    public class NHibernateContext : IContext
    {
        public void AddOrUpdate<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            using (ISession session = FluentNHibertnateSession.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(entity);
                    transaction.Commit();
                }
            }
        }


        public List<TEntity> GetAll<TEntity>() where TEntity : BaseEntity
        {
            using (ISession session = FluentNHibertnateSession.OpenSession())
            {
                return session.Query<TEntity>().ToList();
            }
        }


        public TEntity GetById<TEntity>(Guid id) where TEntity : BaseEntity
        {
            using (ISession session = FluentNHibertnateSession.OpenSession())
            {
                return session.Get<TEntity>(id);
            }
        }


        public void Remove<TEntity>(Guid id) where TEntity : BaseEntity
        {
            using (ISession session = FluentNHibertnateSession.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(session.Load<TEntity>(id));
                    transaction.Commit();
                }
            }
        }
    }
}
