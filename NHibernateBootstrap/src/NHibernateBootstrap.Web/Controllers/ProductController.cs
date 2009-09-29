using System;
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

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Edit(ProductModel productForm)
		{
                Product product = _unitOfWork.CurrentSession
                    .CreateCriteria(typeof(Product))
                    .Add(Restrictions.Eq("Name", productForm.Name))
                    .UniqueResult<Product>();
			product.Category = productForm.Category;
			product.Discontinued = productForm.Discontinued;

//			Product product = new Product
//			                  	{
//			                  		Name = productForm.Name,
//									Category = productForm.Category,
//									Discontinued = productForm.Discontinued
//			                  	};
				_unitOfWork.CurrentSession.Save(product);
			_unitOfWork.Commit();
			return RedirectToAction("Index");
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Edit (Guid id)
		{
			var product = _unitOfWork.CurrentSession.Get<Product>(id);
			ProductModel productModel = CreateModelFrom(product);
			return View(productModel);
		}

		private ProductModel CreateModelFrom(Product product)
		{
			return new ProductModel
			       	{
			       		Name = product.Name,
						Category = product.Category,
						Discontinued = product.Discontinued
			       	};
		}
	}


	public class ProductModel
	{
		public string Name { get; set; }

		public string Category { get; set; }

		public bool Discontinued { get; set; }
	}
}
