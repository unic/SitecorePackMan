namespace Unic.PackMan.Core.Pipelines.AddItem
{
    using Sitecore.Data.Items;
    using Sitecore.Pipelines;

    /// <summary>
    /// Add item pipeline arguments.
    /// </summary>
    public class AddItemPipelineArgs : PipelineArgs
    {
        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        /// <value>
        /// The item.
        /// </value>
        public Item Item { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to add subitems as well.
        /// </summary>
        /// <value>
        ///   <c>true</c> if subitems should be added as well; otherwise, <c>false</c>.
        /// </value>
        public bool AddWithSubItems { get; set; }
    }
}
