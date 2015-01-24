namespace Unic.PackMan.Core.DependencyInjection
{
    using System;
    using Ninject;
    using Ninject.Modules;
    using Sitecore.Reflection;

    /// <summary>
    /// Container factory to resolve classes from the IoC container.
    /// </summary>
    public class ContainerFactory : IFactory
    {
        /// <summary>
        /// The kernel
        /// </summary>
        private static readonly IKernel Kernel;

        /// <summary>
        /// Initializes static members of the <see cref="ContainerFactory"/> class.
        /// </summary>
        static ContainerFactory()
        {
            Kernel = new StandardKernel(new INinjectModule[] { new Config() });
        }

        /// <summary>
        /// Resolves an instance from the container.
        /// </summary>
        /// <typeparam name="T">Type to resolve</typeparam>
        /// <returns>Resolved instance of type</returns>
        public static T Resolve<T>()
        {
            return Kernel.Get<T>();
        }

        /// <summary>
        /// Gets the object from the container.
        /// </summary>
        /// <param name="identifier">The type identifier.</param>
        /// <returns>Resolved instance of type</returns>
        public object GetObject(string identifier)
        {
            return Kernel.Get(Type.GetType(identifier));
        }
    }
}
