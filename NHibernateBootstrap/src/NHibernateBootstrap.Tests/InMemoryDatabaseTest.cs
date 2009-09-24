using System;
using System.Reflection;
using NHibernate;
using NHibernate.ByteCode.Castle;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;
using Environment=NHibernate.Cfg.Environment;

namespace NHibernateBootstrap.Tests
{
    public class InMemoryDatabaseTest : IDisposable
    {
        private static Configuration _configuration;
        private static ISessionFactory _sessionFactory;
        protected ISession session;

        public InMemoryDatabaseTest(Assembly assemblyContainingMapping)
        {
            if (_configuration == null)
            {
                _configuration = new Configuration()
                    .SetProperty(Environment.ReleaseConnections, "on_close")
                    .SetProperty(Environment.Dialect, typeof(SQLiteDialect).AssemblyQualifiedName)
                    .SetProperty(Environment.ConnectionDriver, typeof(SQLite20Driver).AssemblyQualifiedName)
                    .SetProperty(Environment.ConnectionString, "data source=:memory:")
                    .SetProperty(Environment.ProxyFactoryFactoryClass, typeof(ProxyFactoryFactory).AssemblyQualifiedName)
                    .AddAssembly(assemblyContainingMapping);

                _sessionFactory = _configuration.BuildSessionFactory();
            }

            session = _sessionFactory.OpenSession();

            new SchemaExport(_configuration).Execute(true, true, false, session.Connection, Console.Out);
        }



        public void Dispose()
        {
            session.Dispose();
        }
    }
}