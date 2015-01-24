namespace Unic.PackMan.Core.Pipelines.RemoveItem
{
    using Unic.PackMan.Core.Tracking;

    /// <summary>
    /// Remove item from track list pipeline processor.
    /// </summary>
    public class RemoveItemFromTrack
    {
        /// <summary>
        /// The tracking service
        /// </summary>
        private readonly ITrackingService trackingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveItemFromTrack"/> class.
        /// </summary>
        /// <param name="trackingService">The tracking service.</param>
        public RemoveItemFromTrack(ITrackingService trackingService)
        {
            this.trackingService = trackingService;
        }

        /// <summary>
        /// Processes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public void Process(RemoveItemPipelineArgs args)
        {
            this.trackingService.UntrackItem(args.Item);
        }
    }
}
