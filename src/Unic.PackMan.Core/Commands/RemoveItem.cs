namespace Unic.PackMan.Core.Commands
{
    using System.Linq;
    using Pipelines.RemoveItem;
    using Sitecore.Diagnostics;
    using Sitecore.Pipelines;
    using Sitecore.Shell.Framework.Commands;

    public class RemoveItem : Command
    {
        public override void Execute(CommandContext context)
        {
            var item = context.Items.FirstOrDefault();
            Assert.IsNotNull(item, "Item must not be null");

            var pipelineArgs = new RemoveItemPipelineArgs {Item = item};
            CorePipeline.Run("PackMan.RemoveItem", pipelineArgs);
        }
    }
}
