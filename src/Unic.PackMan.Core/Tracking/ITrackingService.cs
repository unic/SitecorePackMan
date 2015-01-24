namespace Unic.PackMan.Core.Tracking
{
    using Sitecore.Data.Items;

    /// <summary>
    /// Service for the tracking logic.
    /// </summary>
    public interface ITrackingService
    {
        /// <summary>
        /// Gets the tracking.
        /// </summary>
        /// <returns>Current tracking object</returns>
        Tracking GetTracking();

        /// <summary>
        /// Determines whether a given item is currently tracked.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Boolean value whether the item is currently tracked</returns>
        bool IsItemTracked(Item item);

        /// <summary>
        /// Adds the item to the track list.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="withSubItems">if set to <c>true</c> the items should be added with subitems.</param>
        void AddItemToTrack(Item item, bool withSubItems = false);

        /// <summary>
        /// Removes the item from the track list.
        /// </summary>
        /// <param name="item">The item.</param>
        void RemoveItemFromTrack(Item item);
    }
}
