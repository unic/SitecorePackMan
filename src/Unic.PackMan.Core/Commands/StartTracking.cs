namespace Unic.PackMan.Core.Commands
{
    using Sitecore.Pipelines;
    using Sitecore.Shell.Framework.Commands;
    using Unic.PackMan.Core.User;

    /// <summary>
    /// Command for starting a tracking session
    /// </summary>
    public class StartTracking : Command
    {
        /// <summary>
        /// Executes the command
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Execute(CommandContext context)
        {
            CorePipeline.Run("PackMan.StartTracking", new PipelineArgs());
            Sitecore.Context.ClientPage.SendMessage(this, "item:refresh");
        }

        /// <summary>
        /// Get the state of the current command
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>State of the current command</returns>
        public override CommandState QueryState(CommandContext context)
        {
            var service = new UserService();
            if (service.IsTrackingEnabled())
            {
                return CommandState.Disabled;
            }
            
            return base.QueryState(context);
        }
    }
}
