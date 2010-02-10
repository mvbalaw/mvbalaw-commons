using NHibernateBootstrap.Core.Persistence;

using NUnit.Framework;

using StructureMap;

namespace NHibernateBootstrap.Tests
{
	[TestFixture]
	public class When_committing_a_unit_of_work_that_was_already_committed
	{

		[TestFixtureSetUp]
		public void TestFixtureSetup()
		{
			Bootstrapper.Bootstrap();
		}
		[Test]
		public void Should_not_blow_up()
		{
			using (IUnitOfWork unitOfWork = ObjectFactory.GetInstance<IUnitOfWork>())
			{
				unitOfWork.Commit();
				unitOfWork.Commit();
			}
		}
	}
}