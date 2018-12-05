using System;
using System.Data;

namespace JDA.Repository
{
    public interface IUnitOfWork : IDisposable
    {        
        IDbConnection Connection { get; }

        IDbTransaction Transaction { get; }

        void Begin();

        void Commit();

        void Rollback();
    }
}
