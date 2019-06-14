namespace DapperRepositoryAndUnitOfWork
{
    using MicroOrm.Pocos.SqlGenerator.Attributes;

    public class Author
    {
        [KeyProperty(Identity = true)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}