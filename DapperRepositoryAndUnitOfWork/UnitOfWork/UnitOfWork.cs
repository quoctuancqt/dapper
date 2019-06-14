namespace DapperRepositoryAndUnitOfWork
{
    using DapperRepositoryAndUnitOfWork.Helper;
    using MicroOrm.Pocos.SqlGenerator;
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.Common;

    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;

        private bool _disposed;

        public UnitOfWork()
        {
            InitConnection();

            InitRepository();
        }

        public IDataRepository<Author> AuthorRepository { get; private set; }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (_connection != null)
                {
                    _connection.Dispose();
                    _connection = null;
                }

                _disposed = true;
            }
        }

        private void InitConnection()
        {
            var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            _connection = factory.CreateConnection();
            _connection.ConnectionString = ConfigurationManager.ConnectionStrings["DapperSampleConnection"].ConnectionString;
        }

        private void InitRepository()
        {
            AuthorRepository = new GenericDataRepository<Author>(_connection, ResolverHelper.GetService<ISqlGenerator<Author>>());
        }
    }
}
