namespace Unic.PackMan.Core.Commands
{
    using System.Collections.Specialized;
    using Configuration;
    using Sitecore;
    using Sitecore.Pipelines;
    using Sitecore.Shell.Framework.Commands;
    using Sitecore.Web.UI.Sheer;
    using Tracking;
    using Unic.PackMan.Core.Pipelines.GeneratePackage;
    using Unic.PackMan.Core.User;

    /// <summary>
    /// Command for generating a item package based on the last session
    /// </summary>
    public class GeneratePackage : Command
    {
        /// <summary>
        /// The user service
        /// </summary>
        private readonly IUserService userService;

        private readonly ITrackingService trackingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneratePackage"/> class.
        /// </summary>
        public GeneratePackage() : this(new UserService(), new TrackingService(new UserService(), new ConfigurationService()))
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneratePackage" /> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        /// <param name="trackingService">The tracking service.</param>
        public GeneratePackage(IUserService userService, ITrackingService trackingService)
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
            var parameters = new NameValueCollection();
            Context.ClientPage.Start(this, "Run", parameters);
        }

        /// <summary>
        /// Queries the state.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Command state for this command.</returns>
        public override CommandState QueryState(CommandContext context)
        {
            if (!this.userService.IsTrackingEnabled())
            {
                return CommandState.Disabled;
            }

            if (!this.trackingService.HasTrackedItems())
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
                SheerResponse.Input("Please enter package name", "Unnamed Package");
                args.WaitForPostBack();
            }
            else if (args.HasResult)
            {
                var pipelineArgs = new GeneratePackagePipelineArgs();

                pipelineArgs.PackageName = args.Result;
                pipelineArgs.PackageAuthor = Context.User.DisplayName;

                CorePipeline.Run("PackMan.GeneratePackage", pipelineArgs);

                if (!string.IsNullOrWhiteSpace(pipelineArgs.DownloadPath))
                {
                    Context.ClientPage.ClientResponse.Download(pipelineArgs.DownloadPath);
                }
                else
                {
                    Context.ClientPage.ClientResponse.Alert(
                        string.Format("Failed to generate package '{0}'", pipelineArgs.PackageName));
                }
            }
        }
    }
}
