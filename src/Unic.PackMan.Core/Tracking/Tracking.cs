namespace Unic.PackMan.Core.Tracking
{
    using System.Collections.Generic;

    /// <summary>
    /// Tracking model
    /// </summary>
    public class Tracking
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Tracking"/> class.
        /// </summary>
        public Tracking()
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
