namespace Unic.PackMan.Core.Tracking
{
    using System.Linq;
    using Configuration;
    using Newtonsoft.Json;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Unic.PackMan.Core.User;
    using Unic.PackMan.Core.Utilities;

    /// <summary>
    /// Service class for the tracking logic
    /// </summary>
    public class TrackingService : ITrackingService
    {
        /// <summary>
        /// The user service
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// The configuration service
        /// </summary>
        private readonly IConfigurationService configurationService;

        /// <summary>
        /// The lock object
        /// </summary>
        private readonly object lockObject = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackingService"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        /// <param name="configurationService">The configuration service.</param>
        public TrackingService(IUserService userService, IConfigurationService configurationService)
        {
            this.configurationService = configurationService;
            this.userService = userService;
        }

        /// <summary>
        /// Gets the tracking.
        /// </summary>
        /// <returns>Current tracking object</returns>
        public virtual TrackingData GetTrackingData()
        {
            var data = this.userService.GetTrackingData();
            return string.IsNullOrWhiteSpace(data) ? null : JsonConvert.DeserializeObject<TrackingData>(data);
        }

        /// <summary>
        /// Determines whether a given item is currently tracked.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// Boolean value whether the item is currently tracked
        /// </returns>
        public virtual bool IsItemTracked(Item item)
        {
            var data = this.GetTrackingData();
            return data != null && data.Items.Any(i => i.Uri == UriHelper.GetUriWithoutQuery(item.Uri.ToString()));
        }

        /// <summary>
        /// Determines whether any items are tracked.
        /// </summary>
        /// <returns>Whether any items are tracked.</returns>
        public virtual bool HasTrackedItems()
        {
            var data = this.GetTrackingData();
            return data != null && data.Items.Any();
        }

        /// <summary>
        /// Adds the item to the track list.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="withSubItems">if set to <c>true</c> the items should be added with subitems.</param>
        public virtual void TrackItem(Item item, bool withSubItems = false)
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
                var data = this.GetTrackingData() ?? new TrackingData();
                foreach (var existingItem in data.Items.Where(i => i.Uri == UriHelper.GetUriWithoutQuery(item.Uri.ToString())).ToList())
                {
                    data.Items.Remove(existingItem);
                }

                data.Items.Add(new TrackedItem
                        {
                            Uri = UriHelper.GetUriWithoutQuery(item.Uri.ToString()),
                            DisplayName = item.DisplayName,
                            Path = item.Paths.FullPath,
                            Icon = item.Appearance.Icon,
                            WithSubItems = withSubItems
                        });

                this.userService.SaveTrackingData(JsonConvert.SerializeObject(data));
            }
        }

        /// <summary>
        /// Removes the item from the track list.
        /// </summary>
        /// <param name="item">The item.</param>
        public virtual void UntrackItem(Item item)
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
                var data = this.GetTrackingData();
                if (data == null) return;

                foreach (var itemToRemove in data.Items.Where(i => i.Uri == UriHelper.GetUriWithoutQuery(item.Uri.ToString())).ToList())
                {
                    data.Items.Remove(itemToRemove);
                }

                this.userService.SaveTrackingData(JsonConvert.SerializeObject(data));
            }
        }
    }
}
