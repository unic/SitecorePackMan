namespace Unic.PackMan.Core.Commands
{
    using Sitecore.Shell.Framework.Commands;
    using Unic.PackMan.Core.DependencyInjection;
    using Unic.PackMan.Core.Tracking;
    using Unic.PackMan.Core.User;

    /// <summary>
    /// Base command
    /// </summary>
    public abstract class CommandBase : Command
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandBase"/> class.
        /// HINT: Use injection with service locator pattern here, because the Sitecore factory does only seem to work for
        /// events and pipelines. Dear Sitecore devs, why ????
        /// </summary>
        protected CommandBase()
        {
            this.UserService = ContainerFactory.Resolve<IUserService>();
            this.TrackingService = ContainerFactory.Resolve<ITrackingService>();
        }

        /// <summary>
        /// Gets the user service.
        /// </summary>
        /// <value>
        /// The user service.
        /// </value>
        protected IUserService UserService { get; private set; }

        /// <summary>
        /// Gets the tracking service.
        /// </summary>
        /// <value>
        /// The tracking service.
        /// </value>
        protected ITrackingService TrackingService { get; private set; }

        /// <summary>
        /// Executes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Execute(CommandContext context)
        {
            // do nothing here
        }

        /// <summary>
        /// Get the state of the current command.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>State of the command.</returns>
        public override CommandState QueryState(CommandContext context)
        {
            if (!this.UserService.IsTrackingEnabled())
            {
                return CommandState.Disabled;
            }

            return CommandState.Enabled;
        }

        /// <summary>
        /// Refreshes the current item.
        /// </summary>
        protected virtual void RefreshItem()
        {
            Sitecore.Context.ClientPage.SendMessage(this, "item:refresh");
        }
    }
}
