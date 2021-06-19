using System;

namespace Unis.Repository
{
    public class DbFactory : IDisposable
    {
        private bool _disposed;
        private Func<RepositoryContext> _instanceFunc;
        private RepositoryContext _dbContext;
        public RepositoryContext DbContext => _dbContext ?? (_dbContext = _instanceFunc.Invoke());

        public DbFactory(Func<RepositoryContext> dbContextFactory)
        {
            _instanceFunc = dbContextFactory;
        }

        public void Dispose()
        {
            if (!_disposed && _dbContext != null)
            {
                _disposed = true;
                _dbContext.Dispose();
            }
        }
    }
}
