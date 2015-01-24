namespace Unic.PackMan.Core.Tracking
{
    using System.Linq;
    using Newtonsoft.Json;
    using Sitecore.Data;
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
            data.Items.Add(new TrackedItem { Uri = item.Uri.ToString(), WithSubItems = withSubItems });
            this.userService.SaveTrackingList(JsonConvert.SerializeObject(data));
        }

        public void RemoveItemFromTrack(Item item)
        {
            Assert.ArgumentNotNull(item, "item");

            if (!this.userService.IsTrackingEnabled())
            {
                Log.Info("Tracking is disabled, don't add the item", this);
                return;
            }

            var data = this.GetTracking();
            if (data == null) return;

            var itemsToRemove = data.Items.Where(i => new ItemUri(i.Uri).ItemID.ToString() == item.ID.ToString()).ToList();
            foreach (var itemToRemove in itemsToRemove)
            {
                data.Items.Remove(itemToRemove);
            }
            
            this.userService.SaveTrackingList(JsonConvert.SerializeObject(data));
        }
    }
}
