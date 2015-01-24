namespace Unic.PackMan.Core.DependencyInjection
{
    using Configuration;
    using Ninject.Modules;
    using Unic.PackMan.Core.PackageGeneration;
    using Unic.PackMan.Core.Tracking;
    using Unic.PackMan.Core.User;

    /// <summary>
    /// Ninject module config.
    /// </summary>
    public class Config : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            this.Bind<IUserService>().To<UserService>();
            this.Bind<ITrackingService>().To<TrackingService>();
            this.Bind<IConfigurationService>().To<ConfigurationService>();
            this.Bind<IPackageGenerationService>().To<PackageGenerationService>();
        }
    }
}
