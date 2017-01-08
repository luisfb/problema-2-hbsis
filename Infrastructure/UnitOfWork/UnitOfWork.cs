using System.Data;
using System.Data.Entity;

namespace Infrastructure.UnitOfWork
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private bool _disposed = false;
        public DbContextTransaction Transaction { get; private set; }
        public DbContext Context { get; private set; }

        public UnitOfWork(DbContext context)
        {
            Context = context;
        }

        public UnitOfWork()
        {
            Context = new Context();
        }

        public void Commit()
        {
            //Context.SaveChanges();
            Transaction?.Commit();
        }

        public void Rollback()
        {
            Transaction?.Rollback();
        }

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            Transaction = Context.Database.CurrentTransaction ?? Context.Database.BeginTransaction(isolationLevel);
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            Context.Database.Connection.Close();
            Context.Database.Connection.Dispose();
            Transaction?.Dispose();
            Context.Dispose();
            _disposed = true;
        }
    }
}
