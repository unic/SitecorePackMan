namespace Unic.PackMan.Core.Pipelines.GeneratePackage
{
    using Sitecore.Data;
    using Unic.PackMan.Core.Tracking;

    public class GetItemsFromUserSession
    {
        /// <summary>
        /// The tracking service
        /// </summary>
        private readonly ITrackingService trackingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetItemsFromUserSession"/> class.
        /// </summary>
        /// <param name="trackingService">The tracking service.</param>
        public GetItemsFromUserSession(ITrackingService trackingService)
        {
            this.trackingService = trackingService;
        }

        /// <summary>
        /// Generates the package
        /// </summary>
        /// <param name="args">The arguments.</param>
        public void Process(GeneratePackagePipelineArgs args)
        {
            var tracking = this.trackingService.GetTracking();
            foreach (var trackingItem in tracking.Items)
            {
                var itemUri = new ItemUri(trackingItem.Uri);
                if (!trackingItem.WithSubItems)
                {
                    args.PackageItems.Add(itemUri);
                    continue;
                }

                // Handle recursion stuff
            }
        }
    }
}
