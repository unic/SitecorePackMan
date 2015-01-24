namespace Unic.PackMan.Core.Events
{
    using System;
    using Sitecore.Data.Items;
    using Sitecore.Events;
    using Unic.PackMan.Core.Tracking;

    public class ItemHandler
    {
        private readonly ITrackingService trackingService;

        public ItemHandler(ITrackingService trackingService)
        {
            this.trackingService = trackingService;
        }

        public void OnItemSaved(object sender, EventArgs args)
        {
            if (!this.trackingService.IsTrackingEnabled()) return;
            
            var item = Event.ExtractParameter(args, 0) as Item;
            if (item == null) return;

            this.trackingService.AddItemToTrack(item);
        }
    }
}
