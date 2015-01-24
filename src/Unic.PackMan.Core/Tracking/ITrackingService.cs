namespace Unic.PackMan.Core.Tracking
{
    using Sitecore.Data.Items;

    public interface ITrackingService
    {
        Tracking GetTracking();

        void AddItemToTrack(Item item, bool withSubItems = false);

        void RemoveItemFromTrack(Item item);
    }
}
