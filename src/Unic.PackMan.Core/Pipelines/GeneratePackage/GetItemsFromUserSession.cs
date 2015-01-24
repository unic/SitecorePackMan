namespace Unic.PackMan.Core.Pipelines.GeneratePackage
{
    using Sitecore.Configuration;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Unic.PackMan.Core.Tracking;

    /// <summary>
    /// Adds the items of the last tracking session of the user
    /// </summary>
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
                    if (!args.PackageItems.Contains(itemUri))
                    {
                        args.PackageItems.Add(itemUri);
                    }

                    continue;
                }

                // Handle recursion stuff
                var item = Factory.GetDatabase(itemUri.DatabaseName).GetItem(itemUri.ItemID);
                this.AddItemWithChildren(item, args);
            }
        }

        /// <summary>
        /// Adds the item with children.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="args">The arguments.</param>
        public void AddItemWithChildren(Item item, GeneratePackagePipelineArgs args)
        {
            if (!args.PackageItems.Contains(item.Uri))
            {
                args.PackageItems.Add(item.Uri);
            }

            foreach (Item child in item.GetChildren())
            {
                this.AddItemWithChildren(child, args);
            }
        }
    }
}
