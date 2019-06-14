namespace DapperConsoleDemo
{
    using Dapper;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.Common;
    using System.Linq;

    public class AuthorRepository
    {
        private readonly IDbConnection _connection;

        public AuthorRepository()
        {
            var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            _connection = factory.CreateConnection();
            _connection.ConnectionString = ConfigurationManager.ConnectionStrings["DapperSampleConnection"].ConnectionString;
            _connection.Open();
        }

        public IEnumerable<Author> Get()
        {
            var sql = @"select * from Author";
            return _connection.Query<Author>(sql);
        }

        public Author GetById(object id)
        {
            var sql = "select * from Author where Id = @Id";
            return _connection.Query<Author>(sql, new { Id = id }).FirstOrDefault();
        }

        public void Insert(Author author)
        {
            var sql = "insert into Author values(@Name, @Email);" +
                "select Scope_Identity()";
            var id = _connection.Query<int>(sql, author).Single();
            author.Id = id;
        }

        public void Update(Author author)
        {
            var sql = "update Author " +
                "set Name = @name, Email = @email " +
                "where Id = @id";
            _connection.Execute(sql, author);
        }

        public void Delete(int id)
        {
            var sql = @"delete Author where Id = @Id";
            _connection.Execute(sql, new { Id = id });
        }
    }
}