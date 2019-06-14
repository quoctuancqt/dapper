namespace DapperRepositoryAndUnitOfWork
{
    using System;
    using System.Data;

    public class DataConnection : IDisposable
    {
        #region Properties
        protected IDbConnection _connection;
        protected IDbConnection Connection
        {
            get
            {
                if (_connection.State != ConnectionState.Open && _connection.State != ConnectionState.Connecting)
                    _connection.Open();

                return _connection;
            }
        }
        #endregion

        public DataConnection(IDbConnection connection)
        {
            _connection = connection;
        }

        public void Dispose()
        {
            if (_connection != null && _connection.State != ConnectionState.Closed)
                _connection.Close();
        }
    }
}
