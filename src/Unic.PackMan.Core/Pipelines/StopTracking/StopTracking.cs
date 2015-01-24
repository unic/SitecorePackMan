namespace Unic.PackMan.Core.Pipelines.StopTracking
{
    using Sitecore.Pipelines;
    using Unic.PackMan.Core.Tracking;

    public class StopTracking
    {
        private readonly ITrackingService trackingService;

        public StopTracking(ITrackingService trackingService)
        {
            this.trackingService = trackingService;
        }

        public void Process(PipelineArgs args)
        {
            this.trackingService.StopTracking();
        }
    }
}
