using System.Web.Mvc;
using NHibernateBootstrap.Core.Persistence;

namespace NHibernateBootstrap.Web.Controllers
{
	public class BuildDatabaseController : Controller
	{

		private readonly IDatabaseBuilder _databaseBuilder;

		public BuildDatabaseController(IDatabaseBuilder databaseBuilder)
		{
			_databaseBuilder = databaseBuilder;
		}

		public ActionResult Index()
		{
			_databaseBuilder.RebuildDatabase();
			return View();
		}

	}
}

