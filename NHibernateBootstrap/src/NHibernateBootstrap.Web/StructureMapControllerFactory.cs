using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace NHibernateBootstrap.Web
{
	public class StructureMapControllerFactory : DefaultControllerFactory
	{

		protected override IController GetControllerInstance(RequestContext requestContext,  Type controllerType)
		{
			if (controllerType == null)
			{
				return base.GetControllerInstance(requestContext, controllerType);
			}            
			return (IController)StructureMap.ObjectFactory.GetInstance(controllerType);

		}

       
	}
}