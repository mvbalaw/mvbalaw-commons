using System;
using NHibernate;

namespace NHibernateBootstrap.Core.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        ISession CurrentSession { get; }
        void Commit();
    	void Rollback();
    }
}
