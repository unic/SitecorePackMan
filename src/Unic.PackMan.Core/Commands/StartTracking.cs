namespace Unic.PackMan.Core.Commands
{
    using Sitecore.Pipelines;
    using Sitecore.Shell.Framework.Commands;

    /// <summary>
    /// Command for starting a tracking session
    /// </summary>
    public class StartTracking : CommandBase
    {
        /// <summary>
        /// Executes the command
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Execute(CommandContext context)
        {
            CorePipeline.Run("PackMan.StartTracking", new PipelineArgs());
            this.RefreshItem();
        }
    }
}
