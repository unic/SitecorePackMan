namespace Unic.PackMan.Core.DependencyInjection
{
    using Ninject.Modules;
    using Unic.PackMan.Core.Tracking;

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
            this.Bind<ITrackingService>().To<TrackingService>();
        }
    }
}
