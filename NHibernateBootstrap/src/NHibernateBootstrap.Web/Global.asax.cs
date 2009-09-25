using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NHibernateBootstrap.Core;
using NHibernateBootstrap.Core.Domain;
using NHibernateBootstrap.Core.Persistence;
using StructureMap;


namespace NHibernateBootstrap.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        private IUnitOfWork _unitOfWork;

        public static void RegisterRoutes(RouteCollection routes)
        {            
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.IgnoreRoute("favicon.ico");


            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
            );

        }

        protected void Application_Start()
        {
        	ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());
            RegisterRoutes(RouteTable.Routes);
            Bootstrapper.Bootstrap();
			new SchemaExport(ObjectFactory.GetInstance<Configuration>()).Execute(false, true, false);
			CreateInitialData();

        }

        protected void Application_BeginRequest()
        {
            _unitOfWork = ObjectFactory.GetInstance<IUnitOfWork>();   
        }

        protected void Application_EndRequest()
        {
            _unitOfWork.Dispose();        
        }

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
			_sessionFactory = ObjectFactory.GetInstance<ISessionFactory>();

			using (var unitOfWork = new UnitOfWork(_sessionFactory))
			{
				foreach (var product in _products)
				{
					unitOfWork.CurrentSession.Save(product);
				}
				unitOfWork.Commit();
			}
		}

    }
}