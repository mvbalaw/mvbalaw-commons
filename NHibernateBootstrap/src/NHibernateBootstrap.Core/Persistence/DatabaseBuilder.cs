using NHibernateBootstrap.Core.Domain;

namespace NHibernateBootstrap.Core.Persistence
{
	public class DatabaseBuilder : IDatabaseBuilder
	{
		private readonly Product[] _products = new[]
		                                       	{
		                                       		new Product {Name = "Melon", Category = "Fruits"},
		                                       		new Product {Name = "Pear", Category = "Fruits"},
		                                       		new Product {Name = "Milk", Category = "Beverages"},
		                                       		new Product {Name = "Coca Cola", Category = "Beverages"},
		                                       		new Product {Name = "Pepsi Cola", Category = "Beverages"}
		                                       	};

		private readonly IUnitOfWork _unitOfWork;

		public DatabaseBuilder (IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public void RebuildDatabase()
		{
			CreateInitialData();
		}

		private void CreateInitialData()
		{
			foreach (var product in _products)
			{
				_unitOfWork.CurrentSession.Save(product);
			}
			_unitOfWork.Commit();
		}

	}
}