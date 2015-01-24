namespace Unic.PackMan.Core.Tracking
{
    public class TrackingService : ITrackingService
    {
        public void StartTracking()
        {
            Sitecore.Diagnostics.Log.Error("START !!!!!!!", this);
        }

        public void StopTracking()
        {
            Sitecore.Diagnostics.Log.Error("STOP !!!!!!!", this);
        }
    }
}
