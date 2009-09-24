using System;
using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NHibernateBootstrap.Core;
using StructureMap;

namespace NHibernateBootstrap.Tests
{
    public class InMemoryDatabaseTest : IDisposable
    {
        private static Configuration _configuration;
        private static ISessionFactory _sessionFactory;
        protected ISession session;

        public InMemoryDatabaseTest()
        {
            Bootstrapper.Bootstrap();


            _configuration = ObjectFactory.GetInstance<Configuration>();
            _sessionFactory = ObjectFactory.GetInstance<ISessionFactory>();

            session = _sessionFactory.OpenSession();

            new SchemaExport(_configuration).Execute(false, true, false, session.Connection, Console.Out);
        }



        public void Dispose()
        {
            session.Dispose();
        }
    }
}