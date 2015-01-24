namespace Unic.PackMan.Core.Tracking
{
    using System.Collections.Generic;

    /// <summary>
    /// Tracking model
    /// </summary>
    public class TrackingData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrackingData"/> class.
        /// </summary>
        public TrackingData()
        {
            this.Items = new List<TrackedItem>();
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public IList<TrackedItem> Items { get; private set; } 
    }
}
