namespace DapperRepositoryAndUnitOfWork.Helper
{
    using Ninject;

    public static class ResolverHelper
    {
        private static IKernel _kernel { set; get; }

        public static void SetProvider(IKernel kernel)
        {
            _kernel = kernel;
        }

        public static T GetService<T>()
           where T : class
        {
            return (T)_kernel.GetService(typeof(T));
        }
    }
}
