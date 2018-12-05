using System;
using System.Data;
using System.Data.SqlClient;

namespace JDA.Repository
{
    public class DalContext : IDalContext
    {
        IDbConnection _connection = null;
        IUnitOfWork _unitOfWork = null;

        public DalContext(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _unitOfWork = new UnitOfWork(_connection);
        }

        public IUnitOfWork UnitOfWork
        {
            get { return _unitOfWork; }
        }

        public IDbConnection DbConnection
        {
            get { return _connection; }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
            _connection.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
