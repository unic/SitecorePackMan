namespace Unic.PackMan.Core.Commands
{
    using Sitecore.Pipelines;
    using Sitecore.Shell.Framework.Commands;

    /// <summary>
    /// Command to stop a tracking session
    /// </summary>
    public class StopTracking : Command
    {
        /// <summary>
        /// Executes the command
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Execute(CommandContext context)
        {
            CorePipeline.Run("PackMan.StopTracking", new PipelineArgs());
        }
    }
}
