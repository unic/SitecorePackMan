namespace Unic.PackMan.Core.DependencyInjection
{
    using Ninject.Modules;
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
        }
    }
}
