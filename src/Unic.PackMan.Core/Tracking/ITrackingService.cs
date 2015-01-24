namespace Unic.PackMan.Core.Tracking
{
    public interface ITrackingService
    {
        bool IsTrackingEnabled();

        Tracking GetTracking();
    }
}
