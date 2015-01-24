namespace Unic.PackMan.Core.Commands
{
    using Pipelines.RemoveItem;
    using Sitecore.Diagnostics;
    using Sitecore.Pipelines;
    using Sitecore.Shell.Framework.Commands;
    using System.Linq;

    /// <summary>
    /// Manually remove an item from the tracking list.
    /// </summary>
    public class RemoveItem : CommandBase
    {
        /// <summary>
        /// Executes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Execute(CommandContext context)
        {
            var item = context.Items.FirstOrDefault();
            Assert.IsNotNull(item, "Item must not be null");

            var pipelineArgs = new RemoveItemPipelineArgs { Item = item };
            CorePipeline.Run("PackMan.RemoveItem", pipelineArgs);

            this.RefreshItem();
        }

        /// <summary>
        /// Get the state of the current command.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>State of the command.</returns>
        public override CommandState QueryState(CommandContext context)
        {
            if (!this.TrackingService.IsItemTracked(context.Items.FirstOrDefault()))
            {
                return CommandState.Disabled;
            }

            return base.QueryState(context);
        }
    }
}
