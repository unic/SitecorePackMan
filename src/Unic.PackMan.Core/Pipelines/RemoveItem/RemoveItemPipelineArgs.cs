namespace Unic.PackMan.Core.Pipelines.RemoveItem
{
    using Sitecore.Data.Items;
    using Sitecore.Pipelines;

    /// <summary>
    /// Remove item pipeline arguments.
    /// </summary>
    public class RemoveItemPipelineArgs : PipelineArgs
    {
        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        /// <value>
        /// The item.
        /// </value>
        public Item Item { get; set; }
    }
}
