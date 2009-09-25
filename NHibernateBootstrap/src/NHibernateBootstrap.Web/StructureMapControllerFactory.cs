using System;
using System.Web.Mvc;

namespace NHibernateBootstrap.Web
{
	public class StructureMapControllerFactory : DefaultControllerFactory
	{

		protected override IController GetControllerInstance(Type controllerType)
		{
			if (controllerType == null)
			{
				return base.GetControllerInstance(controllerType);
			}            
			return (IController)StructureMap.ObjectFactory.GetInstance(controllerType);

		}

       
	}
}