namespace DapperRepositoryAndUnitOfWork
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDataRepository<TEntity>
        where TEntity : new()
    {
        #region Sync
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetWhere(object filters);
        TEntity GetFirst(object filters);
        bool Insert(TEntity instance);
        bool Delete(object key);
        bool Update(TEntity instance);
        #endregion

        #region Async
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetWhereAsync(object filters);
        Task<TEntity> GetFirstAsync(object filters);
        Task<bool> UpdateAsync(TEntity instance);
        Task<bool> InsertAsync(TEntity instance);
        Task<bool> DeleteAsync(object key);
        #endregion
    }
}
