using System;

using Daisy.Dal.Domain;

using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;


namespace Daisy.Dal.Context
{
    public sealed class FluentNHibertnateSession : IDisposable
    {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    _sessionFactory = Fluently.Configure()
                        .Database(MsSqlConfiguration.MsSql2008.ConnectionString(SettingsProvider.GetDbConnectionString()))
                        .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ArticleEntity>())
                        .ExposeConfiguration(BuildSchema)
                        .BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }


        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }


        public void Dispose()
        {
            if (_sessionFactory != null)
            {
                _sessionFactory.Dispose();
            }
        }


        private static void BuildSchema(Configuration config)
        {
            new SchemaUpdate(config).Execute(false, true);
        }
    }
}
