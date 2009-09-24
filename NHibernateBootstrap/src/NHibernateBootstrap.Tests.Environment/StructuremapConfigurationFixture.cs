using NHibernateBootstrap.Core;
using NUnit.Framework;
using StructureMap;

namespace NHibernateBootstrap.Tests.Environment
{
    [TestFixture]
    public class When_configuring_the_application_using_structuremap
    {
        [Test]
        public void should_run_assert_configuration_is_valid_without_exceptions()
        {
            Bootstrapper.Bootstrap();
            ObjectFactory.AssertConfigurationIsValid();
        }

    }
}