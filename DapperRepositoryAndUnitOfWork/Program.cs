using System;
using Ninject;

namespace DapperRepositoryAndUnitOfWork
{
    class Program
    {
        static IUnitOfWork _unitOfWork;

        static void Main(string[] args)
        {
            IKernel kernel = new StandardKernel();

            RegisterDI.Initialize(kernel);

            _unitOfWork = kernel.Get<IUnitOfWork>();

            _unitOfWork.AuthorRepository.Insert(new Author()
            {
                Name = "Messi Lionel",
                Email = "messi@yopmail.com"
            });

            Console.Clear();

            PrintAuthor();

            Console.ReadKey();
        }

        private static void PrintAuthor()
        {
            var authors = _unitOfWork.AuthorRepository.GetAll();

            foreach (var author in authors)
            {
                Console.WriteLine($"Name: {author.Name}, Email: {author.Email}");
            }
        }
    }
}
