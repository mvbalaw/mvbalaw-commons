using NHibernate;

namespace NHibernateBootstrap.Core.Persistence
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ISessionFactory _sessionFactory;
		private readonly ITransaction _transaction;

		public UnitOfWork(ISessionFactory sessionFactory)
		{
			_sessionFactory = sessionFactory;
			CurrentSession = _sessionFactory.OpenSession();
			_transaction = CurrentSession.BeginTransaction();
		}

		public ISession CurrentSession { get; private set; }

		public void Dispose()
		{
			CurrentSession.Close();
			CurrentSession = null;
		}

		public void Commit()
		{
			if (_transaction.IsActive)
				_transaction.Commit();
		}

		public void Rollback()
		{
			_transaction.Rollback();
		}
	}
}