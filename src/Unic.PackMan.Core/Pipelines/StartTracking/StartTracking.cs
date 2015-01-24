namespace Unic.PackMan.Core.Pipelines.StartTracking
{
    using Sitecore.Pipelines;
    using Unic.PackMan.Core.Tracking;

    public class StartTracking
    {
        private readonly ITrackingService trackingService;

        public StartTracking(ITrackingService trackingService)
        {
            this.trackingService = trackingService;
        }

        public void Process(PipelineArgs args)
        {
            this.trackingService.StartTracking();
        }
    }
}
