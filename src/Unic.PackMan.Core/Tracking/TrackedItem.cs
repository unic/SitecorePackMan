namespace Unic.PackMan.Core.Tracking
{
    /// <summary>
    /// An item to track.
    /// </summary>
    public class TrackedItem
    {
        /// <summary>
        /// Gets or sets the URI.
        /// </summary>
        /// <value>
        /// The URI.
        /// </value>
        public string Uri { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to add the item with subitems.
        /// </summary>
        /// <value>
        ///   <c>true</c> if subitems should be added as well; otherwise, <c>false</c>.
        /// </value>
        public bool WithSubItems { get; set; }
    }
}
