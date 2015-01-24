namespace Unic.PackMan.Core.Tracking
{
    using Sitecore.Data.Items;

    /// <summary>
    /// Service for the tracking logic.
    /// </summary>
    public interface ITrackingService
    {
        /// <summary>
        /// Gets the tracking data.
        /// </summary>
        /// <returns>Current tracking data object</returns>
        TrackingData GetTrackingData();

        /// <summary>
        /// Determines whether a given item is currently tracked.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Boolean value whether the item is currently tracked</returns>
        bool IsItemTracked(Item item);

        /// <summary>
        /// Determines whether any items are tracked.
        /// </summary>
        /// <returns>Whether any items are tracked.</returns>
        bool HasTrackedItems();

        /// <summary>
        /// Adds the item to the tracking list.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="withSubItems">if set to <c>true</c> the items should be added with subitems.</param>
        void TrackItem(Item item, bool withSubItems = false);

        /// <summary>
        /// Removes the item from the tracking list.
        /// </summary>
        /// <param name="item">The item.</param>
        void UntrackItem(Item item);
    }
}
