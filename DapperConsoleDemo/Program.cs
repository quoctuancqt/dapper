using System;

namespace DapperConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var authorRep = new AuthorRepository();

            Console.WriteLine("Get Data:");

            var authors = authorRep.Get();

            foreach(var author in authors)
            {
                Console.WriteLine($"Name: {author.Name}, Email: {author.Email}");
            }

            Console.ReadKey();
        }
    }
}
