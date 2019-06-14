using System.Data;
using MicroOrm.Pocos.SqlGenerator;

namespace DapperRepositoryAndUnitOfWork
{
    public sealed class GenericDataRepository<TEntity> : DataRepository<TEntity>, IDataRepository<TEntity>
        where TEntity : new()
    {
        public GenericDataRepository(IDbConnection connection, ISqlGenerator<TEntity> sqlGenerator) : base(connection, sqlGenerator)
        {
        }
    }
}
