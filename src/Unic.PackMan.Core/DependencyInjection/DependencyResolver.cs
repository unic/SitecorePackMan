namespace Unic.PackMan.Core.DependencyInjection
{
    using Ninject;
    using Ninject.Modules;

    /// <summary>
    /// The dependency resolver for Ninject.
    /// </summary>
    public static class DependencyResolver
    {
        /// <summary>
        /// The kernel
        /// </summary>
        private static IKernel kernel;

        /// <summary>
        /// Creates the kernel.
        /// </summary>
        /// <param name="modules">The modules.</param>
        public static void CreateKernel(INinjectModule[] modules)
        {
            kernel = new StandardKernel(modules);
        }

        /// <summary>
        /// Resolves s specific instance.
        /// </summary>
        /// <typeparam name="T">Type to resolve</typeparam>
        /// <returns>Resolved instance</returns>
        public static T Resolve<T>()
        {
            return kernel.Get<T>();
        }
    }
}
