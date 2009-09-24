using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NHibernateBootstrap.Core;
using NUnit.Framework;
using StructureMap;

namespace NHibernateBootstrap.Tests
{
    [TestFixture]
    public class When_generating_the_schema
    {
        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            Bootstrapper.Bootstrap();
        }

        [Test]
        public void should_run_without_exceptions()
        {
            new SchemaExport(ObjectFactory.GetInstance<Configuration>()).Execute(false, true, false);
            
        }
    }
}