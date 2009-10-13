using System;
using System.Web;
using NHibernateBootstrap.Core.Persistence;
using StructureMap;

namespace NHibernateBootstrap.Web
{
	public class NHibernateModule : IHttpModule, IDisposable
	{
		private IUnitOfWork _unitOfWork;

		public void Init(HttpApplication context)
		{
			context.BeginRequest += ContextBeginRequest;
			context.EndRequest += ContextEndRequest;
		}

		private void ContextBeginRequest(object sender, EventArgs e)
		{
			_unitOfWork = ObjectFactory.GetInstance<IUnitOfWork>();
		}

		private void ContextEndRequest(object sender, EventArgs e)
		{
			Dispose();
		}

		public void Dispose()
		{
			_unitOfWork.Commit();
			_unitOfWork.Dispose();
		}
	}
}