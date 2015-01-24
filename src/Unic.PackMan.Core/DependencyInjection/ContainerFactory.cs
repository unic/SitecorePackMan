namespace Unic.PackMan.Core.DependencyInjection
{
    using System;
    using Ninject;
    using Ninject.Modules;

    /// <summary>
    /// Container factory to resolve classes from the IoC container.
    /// </summary>
    public class ContainerFactory
    {
        /// <summary>
        /// The kernel
        /// </summary>
        private readonly IKernel kernel;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerFactory"/> class.
        /// </summary>
        public ContainerFactory()
        {
            this.kernel = new StandardKernel(new INinjectModule[] { new Config() });
        }

        /// <summary>
        /// Gets the object from the container.
        /// </summary>
        /// <param name="identifier">The type identifier.</param>
        /// <returns>Resolved instance of type</returns>
        public object GetObject(string identifier)
        {
            return this.kernel.Get(Type.GetType(identifier));
        }
    }
}
