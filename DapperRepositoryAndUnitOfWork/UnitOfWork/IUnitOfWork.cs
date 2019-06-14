namespace DapperRepositoryAndUnitOfWork
{
    using System;

    public interface IUnitOfWork : IDisposable
    {
        IDataRepository<Author> AuthorRepository { get; }
    }
}
