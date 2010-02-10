using NHibernate.Cfg;
using NHibernate.Criterion;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using NHibernateBootstrap.Core.Domain;
using NHibernateBootstrap.Core.Persistence;
using NUnit.Framework;
using StructureMap;

namespace NHibernateBootstrap.Tests
{
    [TestFixture]
    public class When_doing_crud_operations_for_products
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
        
        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            Bootstrapper.Bootstrap();
            _sessionFactory = ObjectFactory.GetInstance<ISessionFactory>();
        }

        [SetUp]
        public void SetupContext()
        {
            new SchemaExport(ObjectFactory.GetInstance<Configuration>()).Execute(false, true, false);
            CreateInitialData();
        }

        [Test]
        public void Should_be_able_to_add_new_products()
        {
            var product = new Product {Name = "Apple", Category = "Fruits"};
            using (var unitOfWork = new UnitOfWork(_sessionFactory))
            {
                unitOfWork.CurrentSession.Save(product);
                unitOfWork.Commit();
            }

            Product fromDb;
        
            using (var session = _sessionFactory.OpenSession())
            {
                fromDb = session.Get<Product>(product.Id);                
            }

            // Test that the product was successfully inserted
            Assert.IsNotNull(fromDb);
            Assert.AreNotSame(product,fromDb);
            Assert.AreEqual(product.Name, fromDb.Name);
            Assert.AreEqual(product.Category, fromDb.Category);            
        }

        [Test]
        public void Should_be_able_to_update_an_existing_product()
        {
            var product = _products[0];
            product.Name = "Yellow Pear";
            using (var unitOfWork = new UnitOfWork(_sessionFactory))
            {
                unitOfWork.CurrentSession.Update(product);
                unitOfWork.Commit();
            }

            // use session to try to load the product
            using (var unitOfWork = new UnitOfWork(_sessionFactory))
            {
                var fromDb = unitOfWork.CurrentSession.Get<Product>(product.Id);
                Assert.AreEqual(product.Name, fromDb.Name);
            }
        }

        [Test]
        public void Should_be_able_to_remove_an_existing_product()
        {
            var product = _products[0];
            using (var unitOfWork = new UnitOfWork(_sessionFactory))
            {
                unitOfWork.CurrentSession.Delete(product);
                unitOfWork.Commit();
            }

            using (var unitOfWork = new UnitOfWork(_sessionFactory))
            {
                var fromDb = unitOfWork.CurrentSession.Get<Product>(product.Id);
                Assert.IsNull(fromDb);
            }
        }

        [Test]
        public void Should_be_able_to_get_an_existing_product_by_id()
        {
            var session = _sessionFactory.OpenSession();
            var fromDb = session.Get<Product>(_products[1].Id);
            Assert.IsNotNull(fromDb);
            Assert.AreNotSame(_products[1], fromDb);
            Assert.AreEqual(_products[1].Name, fromDb.Name);
        }

        [Test]
        public void Should_be_able_to_get_an_existing_product_by_name()
        {
            Product fromDb;

            using (var session = _sessionFactory.OpenSession())
            {
                fromDb = session.CreateCriteria(typeof(Product))
                    .Add(Restrictions.Eq("Name", _products[1].Name))
                    .UniqueResult<Product>();
            }

            Assert.IsNotNull(fromDb);
            Assert.AreNotSame(_products[1], fromDb);
            Assert.AreEqual(_products[1].Id, fromDb.Id);
        }
    }
}