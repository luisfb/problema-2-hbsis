using System;
using System.Data;
using System.Data.Entity;

namespace Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void Rollback();
        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
        DbContextTransaction Transaction { get; }
        DbContext Context { get; }
    }
}
