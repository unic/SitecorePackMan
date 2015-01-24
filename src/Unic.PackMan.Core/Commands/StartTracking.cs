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

        /// <summary>
        /// Get the state of the current command.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>State of the command.</returns>
        public override CommandState QueryState(CommandContext context)
        {
            if (this.UserService.IsTrackingEnabled())
            {
                return CommandState.Disabled;
            }

            return CommandState.Enabled;
        }
    }
}
