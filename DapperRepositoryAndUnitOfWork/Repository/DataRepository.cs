namespace DapperRepositoryAndUnitOfWork
{
    using Dapper;
    using MicroOrm.Pocos.SqlGenerator;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;

    public abstract class DataRepository<TEntity> : DataConnection, IDataRepository<TEntity>
        where TEntity : new()
    {
        #region Constructors
        public DataRepository(IDbConnection connection, ISqlGenerator<TEntity> sqlGenerator) : base(connection)
        {
            _sqlGenerator = sqlGenerator;
        }
        #endregion

        #region Properties
        public ISqlGenerator<TEntity> _sqlGenerator { get; set; }
        #endregion

        #region Repository sync base actions
        public virtual IEnumerable<TEntity> GetAll()
        {
            var sql = _sqlGenerator.GetSelectAll();
            return _connection.Query<TEntity>(sql);
        }

        public virtual IEnumerable<TEntity> GetWhere(object filters)
        {
            var sql = _sqlGenerator.GetSelect(filters);
            return _connection.Query<TEntity>(sql, filters);
        }

        public virtual TEntity GetFirst(object filters)
        {
            return GetWhere(filters).FirstOrDefault();
        }

        public virtual bool Insert(TEntity instance)
        {
            bool added = false;
            var sql = _sqlGenerator.GetInsert();
            if (_sqlGenerator.IsIdentity)
            {
                var newId = _connection.Query<decimal>(sql, instance).Single();
                added = newId > 0;
                if (added)
                {
                    var newParsedId = Convert.ChangeType(newId, _sqlGenerator.IdentityProperty.PropertyInfo.PropertyType);
                    _sqlGenerator.IdentityProperty.PropertyInfo.SetValue(instance, newParsedId);
                }
            }
            else
            {
                added = _connection.Execute(sql, instance) > 0;
            }

            return added;
        }

        public virtual bool Delete(object key)
        {
            var sql = _sqlGenerator.GetDelete();
            return _connection.Execute(sql, key) > 0;
        }

        public virtual bool Update(TEntity instance)
        {
            var sql = _sqlGenerator.GetUpdate();
            return _connection.Execute(sql, instance) > 0;
        }

        #endregion

        #region Repository async base action
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var sql = _sqlGenerator.GetSelectAll();
            return await _connection.QueryAsync<TEntity>(sql);
        }

        public virtual async Task<IEnumerable<TEntity>> GetWhereAsync(object filters)
        {
            var sql = _sqlGenerator.GetSelect(filters);
            return await _connection.QueryAsync<TEntity>(sql, filters);
        }

        public virtual async Task<TEntity> GetFirstAsync(object filters)
        {
            var sql = _sqlGenerator.GetSelect(filters);
            Task<IEnumerable<TEntity>> queryTask = _connection.QueryAsync<TEntity>(sql, filters);
            IEnumerable<TEntity> data = await queryTask;
            return data.FirstOrDefault();
        }

        public virtual async Task<bool> InsertAsync(TEntity instance)
        {
            bool added = false;
            var sql = _sqlGenerator.GetInsert();

            if (_sqlGenerator.IsIdentity)
            {
                Task<IEnumerable<decimal>> queryTask = _connection.QueryAsync<decimal>(sql, instance);
                IEnumerable<decimal> result = await queryTask;
                var newId = result.Single();
                added = newId > 0;

                if (added)
                {
                    var newParsedId = Convert.ChangeType(newId, _sqlGenerator.IdentityProperty.PropertyInfo.PropertyType);
                    _sqlGenerator.IdentityProperty.PropertyInfo.SetValue(instance, newParsedId);
                }
            }
            else
            {
                Task<IEnumerable<int>> queryTask = _connection.QueryAsync<int>(sql, instance);
                IEnumerable<int> result = await queryTask;
                added = result.Single() > 0;
            }

            return added;
        }

        public virtual async Task<bool> DeleteAsync(object key)
        {
            var sql = _sqlGenerator.GetDelete();
            Task<IEnumerable<int>> queryTask = _connection.QueryAsync<int>(sql, key);
            IEnumerable<int> result = await queryTask;
            return result.SingleOrDefault() > 0;
        }

        public virtual async Task<bool> UpdateAsync(TEntity instance)
        {
            var sql = _sqlGenerator.GetUpdate();
            Task<IEnumerable<int>> queryTask = _connection.QueryAsync<int>(sql, instance);
            IEnumerable<int> result = await queryTask;
            return result.SingleOrDefault() > 0;
        }
        #endregion
    }
}
