namespace Unic.PackMan.Core.Commands
{
    using Pipelines.AddItem;
    using Sitecore.Diagnostics;
    using Sitecore.Pipelines;
    using Sitecore.Shell.Framework.Commands;
    using System.Linq;

    /// <summary>
    /// Command to manually add an item to tracking list.
    /// </summary>
    public class AddItem : CommandBase
    {
        /// <summary>
        /// Executes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Execute(CommandContext context)
        {
            var item = context.Items.FirstOrDefault();
            Assert.IsNotNull(item, "Item must not be null");
            
            var pipelineArgs = new AddItemPipelineArgs { Item = item, AddWithSubItems = false };
            CorePipeline.Run("PackMan.AddItem", pipelineArgs);

            this.RefreshItem();
        }
    }
}
