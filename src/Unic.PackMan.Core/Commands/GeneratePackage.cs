namespace Unic.PackMan.Core.Commands
{
    using System.Collections.Specialized;
    using Sitecore;
    using Sitecore.Pipelines;
    using Sitecore.Shell.Framework.Commands;
    using Sitecore.Web.UI.Sheer;
    using Unic.PackMan.Core.Pipelines.GeneratePackage;

    /// <summary>
    /// Command for generating a item package based on the last session
    /// </summary>
    public class GeneratePackage : Command
    {
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
