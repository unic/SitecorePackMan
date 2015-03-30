namespace Unic.PackMan.Core.Commands
{
    using Sitecore;
    using Sitecore.Globalization;
    using Sitecore.Pipelines;
    using Sitecore.Shell.Framework.Commands;
    using Sitecore.Web.UI.Sheer;
    using System.Collections.Specialized;
    using Unic.PackMan.Core.Pipelines.GeneratePackage;

    /// <summary>
    /// Command for generating a item package based on the last session
    /// </summary>
    public class GeneratePackage : CommandBase
    {
        /// <summary>
        /// Executes the command
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Execute(CommandContext context)
        {
            Context.ClientPage.Start(this, "Run", new NameValueCollection());
        }

        /// <summary>
        /// Get the state of the current command.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>State of the command.</returns>
        public override CommandState QueryState(CommandContext context)
        {
            if (!this.TrackingService.HasTrackedItems())
            {
                return CommandState.Disabled;
            }

            return base.QueryState(context);
        }

        /// <summary>
        /// Runs the package generation
        /// </summary>
        /// <param name="args">The client pipeline arguments.</param>
        protected virtual void Run(ClientPipelineArgs args)
        {
            if (!args.IsPostBack)
            {
                SheerResponse.Input(
                    Translate.Text("Please enter package name"),
                    Translate.Text("Unnamed Package"),
                    @"^(?!\s*$).+",
                    Translate.Text("Invalid package name"),
                    255);

                args.WaitForPostBack();
            }
            else if (args.HasResult)
            {
                var pipelineArgs = new GeneratePackagePipelineArgs
                {
                    PackageName = args.Result,
                    PackageAuthor = Context.User.DisplayName
                };

                CorePipeline.Run("PackMan.GeneratePackage", pipelineArgs);

                if (!string.IsNullOrWhiteSpace(pipelineArgs.DownloadPath))
                {
                    Context.ClientPage.ClientResponse.Download(pipelineArgs.DownloadPath);
                }
                else
                {
                    Context.ClientPage.ClientResponse.Alert(string.Format("Failed to generate package '{0}'", pipelineArgs.PackageName));
                }
            }
        }
    }
}
