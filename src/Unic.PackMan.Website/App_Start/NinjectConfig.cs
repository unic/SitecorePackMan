[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Unic.PackMan.Website.App_Start.NinjectConfig), "Start")]

namespace Unic.PackMan.Website.App_Start
{
    using System.Collections.Generic;
    using System.Linq;
    using Ninject.Modules;
    using Unic.PackMan.Core.DependencyInjection;

    /// <summary>
    /// Ninject configuration
    /// </summary>
    public class NinjectConfig
    {
        /// <summary>
        /// Initialize Ninject IoC container.
        /// </summary>
        public static void Start()
        {
            DependencyResolver.CreateKernel(GetModules().ToArray());
        }

        /// <summary>
        /// Gets the Ninject config modules.
        /// </summary>
        /// <returns>List of config modules</returns>
        private static IEnumerable<INinjectModule> GetModules()
        {
            yield return new Config();
        }
    }
}