namespace Unic.PackMan.Core.Tracking
{
    using Sitecore.Data.Items;

    public interface ITrackingService
    {
        bool IsTrackingEnabled();

        Tracking GetTracking();

        void AddItemToTrack(Item item, bool withSubItems = false);
    }
}
