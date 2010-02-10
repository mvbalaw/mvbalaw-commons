using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NHibernateBootstrap.Core.Domain;
using NHibernateBootstrap.Core.Persistence;
using NUnit.Framework;
using StructureMap;

namespace NHibernateBootstrap.Tests
{
	[TestFixture]
	public class PopulateDatabaseFixture
	{
		private readonly Product[] _products = new[]
                                                   {
                                                       new Product {Name = "Melon", Category = "Fruits"},
                                                       new Product {Name = "Pear", Category = "Fruits"},
                                                       new Product {Name = "Milk", Category = "Beverages"},
                                                       new Product {Name = "Coca Cola", Category = "Beverages"},
                                                       new Product {Name = "Pepsi Cola", Category = "Beverages"}
                                                   };

		private ISessionFactory _sessionFactory;

		private void CreateInitialData()
		{
			using (var unitOfWork = new UnitOfWork(_sessionFactory))
			{
				foreach (var product in _products)
				{
					unitOfWork.CurrentSession.Save(product);
				}
				unitOfWork.Commit();
			}
		}


		[Test]
		public void Should_populate_database_with_product_data()
		{
			Bootstrapper.Bootstrap();
			_sessionFactory = ObjectFactory.GetInstance<ISessionFactory>();
			new SchemaExport(ObjectFactory.GetInstance<Configuration>()).Execute(false, true, false);
			CreateInitialData();
		}


	}
}