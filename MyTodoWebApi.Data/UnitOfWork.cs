
using MyTodoWebApi.ApiModel;
using MyTodoWebApi.Data.Contracts;
using MyTodoWebApi.Data.Helpers;
using System;
namespace MyTodoWebApi.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly customerinfoContext DbContext = new customerinfoContext();

        private IRepositoryProvider RepositoryProvider { get; }

        public UnitOfWork(IRepositoryProvider repositoryProvider)
        {
            repositoryProvider.DbContext = DbContext;
            RepositoryProvider = repositoryProvider;
        }

        #region Repository definition
        public ICustomerRepository Customer { get { return GetEntityRepository<ICustomerRepository>(); } }
        public IUserRepository user { get { return GetEntityRepository<IUserRepository>(); } }

        // public ICustomerRepository Customer => throw new NotImplementedException();

        #endregion

        public void Commit()
        {
            DbContext.SaveChangesAsync();
        }

        private IRepository<T> GetStandardRepository<T>() where T : class
        {
            return RepositoryProvider.GetRepositoryForEntityType<T>();
        }

        private T GetEntityRepository<T>() where T : class
        {
            return RepositoryProvider.GetRepository<T>();
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (DbContext != null)
                {
                    DbContext.Dispose();
                }
            }
        }

        #endregion
    }
}

