using NHibernateBootstrap.Core.Domain;
using StructureMap;

namespace NHibernateBootstrap.Web
{
	public class Bootstrapper : IBootstrapper
	{
		private static bool _hasStarted;

		public virtual void BootstrapStructureMap()
		{
			ObjectFactory.Initialize(x =>
			                         x.Scan(s =>
			                                	{
			                                		s.TheCallingAssembly();
			                                		s.AssemblyContainingType<Product>();
			                                		s.LookForRegistries();
			                                	}	
			                         	)
				);
		}

		public static void Restart()
		{
			if (_hasStarted)
			{
				ObjectFactory.ResetDefaults();
			}
			else
			{
				Bootstrap();
				_hasStarted = true;
			}
		}

		public static void Bootstrap()
		{
			new Bootstrapper().BootstrapStructureMap();
		}
	}
}