namespace Unic.PackMan.Core.Commands
{
    using Sitecore;
    using Sitecore.Pipelines;
    using Sitecore.Shell.Framework.Commands;
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
            var pipelineArgs = new GeneratePackagePipelineArgs();

            // ugly fake code
            var homeItem = Context.ContentDatabase.GetItem("{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}");
            pipelineArgs.PackageItems.Add(homeItem);
            pipelineArgs.PackageName = "Static Packetname";
            pipelineArgs.PackageAuthor = Context.User.DisplayName;

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
