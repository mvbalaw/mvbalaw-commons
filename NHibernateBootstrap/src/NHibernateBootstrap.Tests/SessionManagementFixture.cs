using NHibernate;
using NHibernateBootstrap.Core;
using NUnit.Framework;
using StructureMap;

namespace NHibernateBootstrap.Tests
{
    [TestFixture]
    public class When_using_nhibernate_configured_by_structuremap
    {
        [TestFixtureSetUp]        
        public void FixtureSetup()
        {
            Bootstrapper.Bootstrap();       
        }

        [Test]
        public void should_create_only_one_session_factory()
        {
            var sessionFactory1 = ObjectFactory.GetInstance<ISessionFactory>();
            var sessionFactory2 = ObjectFactory.GetInstance<ISessionFactory>();
            Assert.AreSame(sessionFactory1, sessionFactory2, "There should only be one SessionFactory per application");
        }

        [Test]
        public void should_provide_an_isession()
        {
            var session = ObjectFactory.GetInstance<ISession>();
            Assert.IsInstanceOf(typeof(ISession), session, "GetSession() should return an ISession object");

        }

        [Test]
        public void should_provide_same_isession_for_the_whole_request()
        {
            var session1 = ObjectFactory.GetInstance<ISession>();
            var session2 = ObjectFactory.GetInstance<ISession>();
            Assert.AreSame(session1, session2, "There should only be one ISession per request");
        }

    }
}