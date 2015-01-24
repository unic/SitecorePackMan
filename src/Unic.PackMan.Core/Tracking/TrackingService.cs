namespace Unic.PackMan.Core.Tracking
{
    using Configuration;
    using Newtonsoft.Json;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Unic.PackMan.Core.User;

    public class TrackingService : ITrackingService
    {
        private readonly IUserService userService;
        private readonly IConfigurationService configurationService;

        public TrackingService(IUserService userService, IConfigurationService configurationService)
        {
            this.configurationService = configurationService;
            this.userService = userService;
        }

        public bool IsTrackingEnabled()
        {
            return this.userService.IsTrackingEnabled();
        }

        public Tracking GetTracking()
        {
            var data = this.userService.GetTrackingList();
            return string.IsNullOrWhiteSpace(data) ? null : JsonConvert.DeserializeObject<Tracking>(data);
        }

        public void AddItemToTrack(Item item, bool withSubItems = false)
        {
            Assert.ArgumentNotNull(item, "item");
            
            if (!this.IsTrackingEnabled())
            {
                Log.Warn("Tracking is disabled, don't add the item", this);
                return;
            }

            if (!this.configurationService.IsItemIncluded(item))
            {
                Log.Debug("Item is excluded, don't add the item", this);
                return;
            }

            var data = this.GetTracking() ?? new Tracking();
            data.Items.Add(new TrackedItem { Id = item.ID.ToString(), WithSubItems = withSubItems });
            this.userService.SaveTrackingList(JsonConvert.SerializeObject(data));
        }
    }
}
