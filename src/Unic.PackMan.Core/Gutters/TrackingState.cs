namespace Unic.PackMan.Core.Gutters
{
    using System.Collections.Generic;
    using System.Linq;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Globalization;
    using Sitecore.Shell.Applications.ContentEditor.Gutters;
    using Unic.PackMan.Core.Configuration;
    using Unic.PackMan.Core.Tracking;
    using Unic.PackMan.Core.User;

    /// <summary>
    /// Gutter icon to inform the user about the item state in the 
    /// </summary>
    public class TrackingState : GutterRenderer
    {
        /// <summary>
        /// The tracked items
        /// </summary>
        private readonly IList<TrackedItem> trackedItems;

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackingState"/> class.
        /// </summary>
        public TrackingState()
            : this(new TrackingService(new UserService(), new ConfigurationService()))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackingState"/> class.
        /// </summary>
        /// <param name="trackingService">The tracking service.</param>
        public TrackingState(ITrackingService trackingService)
        {
            var tracking = trackingService.GetTracking();
            if (tracking != null) this.trackedItems = tracking.Items;
        }

        /// <summary>
        /// Gets the icon descriptor.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Gets the icon for this item</returns>
        protected override GutterIconDescriptor GetIconDescriptor(Item item)
        {
            if (this.trackedItems == null) return null;

            var trackingItem = this.trackedItems.FirstOrDefault(
                x =>
                    {
                        var uri = new ItemUri(x.Uri);
                        return uri.DatabaseName == item.Database.Name && uri.ItemID == item.ID;
                    });

            if (trackingItem == null) return null;

            if (trackingItem.WithSubItems)
            {
                return new GutterIconDescriptor
                {
                    Icon = "Office/32x32/arrow_fork.png",
                    Tooltip = Translate.Text("Included in Item package with it's children")
                };
            }

            return new GutterIconDescriptor
            {
                Icon = "Office/32x32/arrow_right.png",
                Tooltip = Translate.Text("Included in Item package")
            };
        }
    }
}
