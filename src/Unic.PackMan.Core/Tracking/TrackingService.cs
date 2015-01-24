namespace Unic.PackMan.Core.Tracking
{
    using Newtonsoft.Json;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Unic.PackMan.Core.User;

    public class TrackingService : ITrackingService
    {
        private readonly IUserService userService;

        public TrackingService(IUserService userService)
        {
            this.userService = userService;
        }

        public Tracking GetTracking()
        {
            var data = this.userService.GetTrackingList();
            return string.IsNullOrWhiteSpace(data) ? null : JsonConvert.DeserializeObject<Tracking>(data);
        }

        public void AddItemToTrack(Item item, bool withSubItems = false)
        {
            Assert.ArgumentNotNull(item, "item");
            
            if (!this.userService.IsTrackingEnabled())
            {
                Log.Info("Tracking is disabled, don't add the item", this);
                return;
            }

            var data = this.GetTracking() ?? new Tracking();
            data.Items.Add(new TrackedItem { Id = item.ID.ToString(), WithSubItems = withSubItems });
            this.userService.SaveTrackingList(JsonConvert.SerializeObject(data));
        }
    }
}
