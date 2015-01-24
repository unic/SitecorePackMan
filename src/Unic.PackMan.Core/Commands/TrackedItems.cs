namespace Unic.PackMan.Core.Commands
{
    using Sitecore.Shell.Framework.Commands;

    /// <summary>
    /// Command to show all the currently tracked items.
    /// </summary>
    public class TrackedItems : CommandBase
    {
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
    }
}
