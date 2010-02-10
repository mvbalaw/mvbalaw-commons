

using System.Net;
using System.Web.Mvc;

namespace NHibernateBootstrap.Web.Controllers
{
	public class ErrorController : Controller
	{
//        [AcceptVerbs(HttpVerbs.Get)]
//        public ViewResult Unknown()
//        {
//            Response.StatusCode = (int)HttpStatusCode.InternalServerError;
//            return View("Unknown");
//        }

		[AcceptVerbs(HttpVerbs.Get)]
		public ViewResult NotFound(string path)
		{
			Response.StatusCode = (int)HttpStatusCode.NotFound;
			return View("NotFound", path);
		}
	}
}