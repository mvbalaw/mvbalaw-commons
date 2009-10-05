using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NHibernateBootstrap.Core.Persistence;
using StructureMap;


namespace NHibernateBootstrap.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
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
			ObjectFactory.GetInstance<IDatabaseBuilder>().RebuildDatabase();            
        }

    }
}