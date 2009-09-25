using System.Collections.Generic;
using System.Web.Mvc;
using NHibernate.Criterion;
using NHibernateBootstrap.Core.Domain;
using NHibernateBootstrap.Core.Persistence;

namespace NHibernateBootstrap.Web.Controllers
{
	public class ProductController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public ProductController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public ActionResult Index()
		{
			
			var productList = _unitOfWork.CurrentSession
					.CreateCriteria(typeof(Product))
					.List<Product>();

			return View(productList);

		}
	}
}
