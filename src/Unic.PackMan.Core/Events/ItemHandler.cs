namespace Unic.PackMan.Core.Events
{
    using Sitecore.Data.Items;
    using Sitecore.Events;
    using Sitecore.Pipelines;
    using System;
    using Unic.PackMan.Core.Pipelines.AddItem;
    using Unic.PackMan.Core.Pipelines.RemoveItem;

    public class ItemHandler
    {
        public void OnItemSaved(object sender, EventArgs args)
        {
            var item = Event.ExtractParameter(args, 0) as Item;
            if (item == null) return;

            var pipelineArgs = new AddItemPipelineArgs { Item = item, AddWithSubItems = false };
            CorePipeline.Run("PackMan.AddItem", pipelineArgs);
        }

        public void OnItemDeleted(object sender, EventArgs args)
        {
            var item = Event.ExtractParameter(args, 0) as Item;
            if (item == null) return;

            var pipelineArgs = new RemoveItemPipelineArgs{ Item = item };
            CorePipeline.Run("PackMan.RemoveItem", pipelineArgs);
        }
    }
}
