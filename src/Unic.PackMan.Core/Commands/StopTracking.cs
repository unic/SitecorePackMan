namespace Unic.PackMan.Core.Commands
{
    using Sitecore.Data;
    using Sitecore.Globalization;
    using Sitecore.Pipelines;
    using Sitecore.Shell.Framework.Commands;
    using Sitecore.Web.UI.Sheer;
    using System.Collections.Specialized;
    using Unic.PackMan.Core.Configuration;
    using Unic.PackMan.Core.Tracking;
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
        /// The tracking service
        /// </summary>
        private readonly ITrackingService trackingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="StopTracking"/> class.
        /// HINT: Use poor man dependency injection here, because the Sitecore factory does only seem to work for
        /// events and pipelines. Dear Sitecore devs, why??
        /// </summary>
        public StopTracking() : this(new UserService(), new TrackingService(new UserService(), new ConfigurationService()))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StopTracking" /> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        /// <param name="trackingService">The tracking service.</param>
        public StopTracking(IUserService userService, ITrackingService trackingService)
        {
            this.trackingService = trackingService;
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
                var trackingItems = this.trackingService.GetTracking();

                CorePipeline.Run("PackMan.StopTracking", new PipelineArgs());
                Sitecore.Context.ClientPage.SendMessage(this, "item:refresh");

                if (trackingItems == null) return;
                foreach (var item in trackingItems.Items)
                {
                    var message = Message.Parse(this, "item:updategutter");
                    message.Arguments.Add("id", new ItemUri(item.Uri).ItemID.ToString());
                    Sitecore.Context.ClientPage.SendMessage(message);
                }
            }
        }
    }
}
