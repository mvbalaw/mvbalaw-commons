using NHibernate;
using NHibernate.ByteCode.Castle;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernateBootstrap.Core.Domain;
using StructureMap.Attributes;
using StructureMap.Configuration.DSL;
using Environment=NHibernate.Cfg.Environment;

namespace NHibernateBootstrap.Core.Persistence
{
    public class NHibernateRegistry : Registry
    {
        public NHibernateRegistry()
        {
            var cfg = new Configuration()
                .SetProperty(Environment.ReleaseConnections, "on_close")
                .SetProperty(Environment.Dialect, typeof(SQLiteDialect).AssemblyQualifiedName)
                .SetProperty(Environment.ConnectionDriver, typeof(SQLite20Driver).AssemblyQualifiedName)
                .SetProperty(Environment.ConnectionString, "data source=bootstrap.sqlite;Version=3")
                .SetProperty(Environment.ProxyFactoryFactoryClass, typeof(ProxyFactoryFactory).AssemblyQualifiedName)
                .AddAssembly(typeof(Blog).Assembly);

            var sessionFactory = cfg.BuildSessionFactory();

            ForRequestedType<Configuration>().AsSingletons().TheDefault.IsThis(cfg);

            ForRequestedType<ISessionFactory>().AsSingletons()
                .TheDefault.IsThis(sessionFactory);

            ForRequestedType<ISession>().CacheBy(InstanceScope.Hybrid)
                .TheDefault.Is.ConstructedBy(ctx => ctx.GetInstance<ISessionFactory>().OpenSession());

            ForRequestedType<IUnitOfWork>().CacheBy(InstanceScope.Hybrid)
                .TheDefaultIsConcreteType<UnitOfWork>();

        	ForRequestedType<IDatabaseBuilder>().TheDefaultIsConcreteType<DatabaseBuilder>();

        }

    }
}