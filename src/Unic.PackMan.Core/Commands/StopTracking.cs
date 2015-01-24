namespace Unic.PackMan.Core.Commands
{
    using System.Collections.Specialized;
    using Sitecore.Globalization;
    using Sitecore.Pipelines;
    using Sitecore.Shell.Framework.Commands;
    using Sitecore.Web.UI.Sheer;
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
        /// Initializes a new instance of the <see cref="StopTracking" /> class.
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
            Sitecore.Context.ClientPage.Start(this, "Run", new NameValueCollection());
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

        /// <summary>
        /// Runs the short URL generation.
        /// </summary>
        /// <param name="args">The client pipeline arguments.</param>
        protected void Run(ClientPipelineArgs args)
        {
            if (!args.IsPostBack)
            {
                SheerResponse.Confirm(Translate.Text("Are you sure you want to stop tracking? Tracked items will be lost. If you don't want to loose your tracked items, please generate the package first"));
                args.WaitForPostBack();
            }
            else if (args.Result == "yes")
            {
                CorePipeline.Run("PackMan.StopTracking", new PipelineArgs());
                Sitecore.Context.ClientPage.SendMessage(this, "item:refresh");
            }
        }
    }
}
