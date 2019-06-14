using DapperRepositoryAndUnitOfWork.Helper;
using MicroOrm.Pocos.SqlGenerator;
using Ninject;
using System.Reflection;

namespace DapperRepositoryAndUnitOfWork
{
    public class RegisterDI
    {
        public static void Initialize(IKernel kernel)
        {
            kernel.Load(Assembly.GetExecutingAssembly());
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            kernel.Bind<ISqlGenerator<Author>>().To<SqlGenerator<Author>>();
            ResolverHelper.SetProvider(kernel);
        }
    }
}
