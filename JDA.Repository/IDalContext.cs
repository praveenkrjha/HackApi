using System;
using System.Data;

namespace JDA.Repository
{
    public interface IDalContext : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }

        IDbConnection DbConnection { get; }
    }
}
