using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.ByteCode.Castle;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernateBootstrap.Core.Domain;
using StructureMap.Configuration.DSL;
using Environment=NHibernate.Cfg.Environment;

namespace NHibernateBootstrap.Core.Persistence
{
    public class NHibernateRegistry : Registry
    {
        public NHibernateRegistry()
        {
            
             /* var cfg = new Configuration()
                .SetProperty(Environment.ReleaseConnections, "on_close")
                .SetProperty(Environment.Dialect, typeof(SQLiteDialect).AssemblyQualifiedName) // Fluently.Configure.Database
                .SetProperty(Environment.ConnectionDriver, typeof(SQLite20Driver).AssemblyQualifiedName)
                .SetProperty(Environment.ConnectionString, "data source=|DataDirectory|bootstrap.sqlite;Version=3")
                .SetProperty(Environment.ProxyFactoryFactoryClass, typeof(ProxyFactoryFactory).AssemblyQualifiedName)
                .AddAssembly(typeof(Blog).Assembly);
              */

            var cfg = Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.UsingFile("|DataDirectory|bootstrap.sqlite"))
                .Mappings(m => m.AutoMappings
                                   .Add(AutoMap.AssemblyOf<Product>()
                                            .Where(t => t.Namespace == "NHibernateBootstrap.Core.Domain")))
                .ExposeConfiguration(c => c.SetProperty(Environment.ReleaseConnections, "on_close"))
                .ExposeConfiguration(c => c.SetProperty(Environment.ProxyFactoryFactoryClass, typeof(ProxyFactoryFactory).AssemblyQualifiedName))
                .BuildConfiguration();

            var sessionFactory = cfg.BuildSessionFactory();

            For<Configuration>().Singleton().Use(cfg);

            For<ISessionFactory>().Singleton().Use(sessionFactory);

            
            For<ISession>().HybridHttpOrThreadLocalScoped()
                .Use(ctx => ctx.GetInstance<ISessionFactory>().OpenSession());

            For<IUnitOfWork>().HybridHttpOrThreadLocalScoped()
                .Use<UnitOfWork>();

        	For<IDatabaseBuilder>().Use<DatabaseBuilder>();

        }

    }
}