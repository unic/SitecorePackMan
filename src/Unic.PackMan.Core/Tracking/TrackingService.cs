namespace Unic.PackMan.Core.Tracking
{
    using System.Linq;
    using Configuration;
    using Newtonsoft.Json;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Unic.PackMan.Core.User;

    public class TrackingService : ITrackingService
    {
        private readonly IUserService userService;

        private readonly IConfigurationService configurationService;

        private readonly object lockObject = new object();

        public TrackingService(IUserService userService, IConfigurationService configurationService)
        {
            this.configurationService = configurationService;
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
                Log.Warn("Tracking is disabled, don't add the item", this);
                return;
            }

            if (!this.configurationService.IsItemIncluded(item))
            {
                Log.Debug("The item is not in the include list, don't add the item", this);
                return;
            }

            lock (this.lockObject)
            {
                var data = this.GetTracking() ?? new Tracking();
                data.Items.Add(new TrackedItem { Uri = item.Uri.ToString(), WithSubItems = withSubItems });
                this.userService.SaveTrackingList(JsonConvert.SerializeObject(data));
            }
        }

        public void RemoveItemFromTrack(Item item)
        {
            Assert.ArgumentNotNull(item, "item");

            if (!this.userService.IsTrackingEnabled())
            {
                Log.Warn("Tracking is disabled, don't remove the item", this);
                return;
            }

            if (!this.configurationService.IsItemIncluded(item))
            {
                Log.Debug("The item is not in the include list, don't remove the item", this);
                return;
            }

            lock (this.lockObject)
            {
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
}
