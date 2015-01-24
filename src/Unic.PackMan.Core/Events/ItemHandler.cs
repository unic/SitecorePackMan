namespace Unic.PackMan.Core.Events
{
    using System;
    using Sitecore.Data.Items;
    using Sitecore.Events;
    using Unic.PackMan.Core.Tracking;
    using Unic.PackMan.Core.User;

    public class ItemHandler
    {
        private readonly ITrackingService trackingService;

        private readonly IUserService userService;

        public ItemHandler(ITrackingService trackingService, IUserService userService)
        {
            this.userService = userService;
            this.trackingService = trackingService;
        }

        public void OnItemSaved(object sender, EventArgs args)
        {
            if (!this.userService.IsTrackingEnabled()) return;
            
            var item = Event.ExtractParameter(args, 0) as Item;
            if (item == null) return;

            this.trackingService.AddItemToTrack(item);
        }
    }
}
