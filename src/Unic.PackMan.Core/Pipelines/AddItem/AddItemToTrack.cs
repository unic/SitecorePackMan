namespace Unic.PackMan.Core.Pipelines.AddItem
{
    using Unic.PackMan.Core.Tracking;

    /// <summary>
    /// Add item to track list pipeline processor
    /// </summary>
    public class AddItemToTrack
    {
        /// <summary>
        /// The tracking service
        /// </summary>
        private readonly ITrackingService trackingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddItemToTrack"/> class.
        /// </summary>
        /// <param name="trackingService">The tracking service.</param>
        public AddItemToTrack(ITrackingService trackingService)
        {
            this.trackingService = trackingService;
        }

        /// <summary>
        /// Processes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public void Process(AddItemPipelineArgs args)
        {
            this.trackingService.AddItemToTrack(args.Item, args.AddWithSubItems);
        }
    }
}
