namespace Unic.PackMan.Core.Commands
{
    using Sitecore.Pipelines;
    using Sitecore.Shell.Framework.Commands;
    using Unic.PackMan.Core.User;

    /// <summary>
    /// Command to stop a tracking session
    /// </summary>
    public class StopTracking : Command
    {
        /// <summary>
        /// The user service
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="StopTracking"/> class.
        /// HINT: Use poor man dependency injection here, because the Sitecore factory does only seem to work for
        /// events and pipelines. Dear Sitecore devs, why??
        /// </summary>
        public StopTracking() : this(new UserService())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StopTracking"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        public StopTracking(IUserService userService)
        {
            this.userService = userService;
        }
        
        /// <summary>
        /// Executes the command
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Execute(CommandContext context)
        {
            CorePipeline.Run("PackMan.StopTracking", new PipelineArgs());
            Sitecore.Context.ClientPage.SendMessage(this, "item:refresh");
        }

        /// <summary>
        /// Get the state of the current command
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>State of the current command</returns>
        public override CommandState QueryState(CommandContext context)
        {
            if (!this.userService.IsTrackingEnabled())
            {
                return CommandState.Disabled;
            }

            return base.QueryState(context);
        }
    }
}
