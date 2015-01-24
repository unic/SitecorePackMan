namespace Unic.PackMan.Core.Commands
{
    using System.Linq;
    using Pipelines.AddItem;
    using Sitecore.Diagnostics;
    using Sitecore.Pipelines;
    using Sitecore.Shell.Framework.Commands;

    /// <summary>
    /// Command to manually add an item to tracking list with subitems.
    /// </summary>
    public class AddItemWithSubitems : CommandBase
    {
        /// <summary>
        /// Executes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Execute(CommandContext context)
        {
            var item = context.Items.FirstOrDefault();
            Assert.IsNotNull(item, "Item must not be null");

            var pipelineArgs = new AddItemPipelineArgs { Item = item, AddWithSubItems = true };
            CorePipeline.Run("PackMan.AddItem", pipelineArgs);

            this.RefreshItem();
        }
    }
}
