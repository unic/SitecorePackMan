namespace Unic.PackMan.Core.Commands
{
    using Sitecore.Pipelines;
    using Sitecore.Shell.Framework.Commands;
    using Unic.PackMan.Core.User;

    /// <summary>
    /// Command for starting a tracking session
    /// </summary>
    public class StartTracking : Command
    {
        /// <summary>
        /// The user service
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="StartTracking"/> class.
        /// HINT: Use poor man dependency injection here, because the Sitecore factory does only seem to work for
        /// events and pipelines. Dear Sitecore devs, why??
        /// </summary>
        public StartTracking() : this(new UserService())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StartTracking"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        public StartTracking(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Execute(CommandContext context)
        {
            CorePipeline.Run("PackMan.StartTracking", new PipelineArgs());
            Sitecore.Context.ClientPage.SendMessage(this, "item:refresh");
        }

        /// <summary>
        /// Get the state of the current command
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>State of the current command</returns>
        public override CommandState QueryState(CommandContext context)
        {
            if (this.userService.IsTrackingEnabled())
            {
                return CommandState.Disabled;
            }
            
            return base.QueryState(context);
        }
    }
}
