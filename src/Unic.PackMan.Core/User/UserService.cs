namespace Unic.PackMan.Core.User
{
    using Sitecore.Security;

    /// <summary>
    /// Service class for user logic
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        /// The tracking enabled profile key
        /// </summary>
        private const string TrackingEnabledProfileKey = "packman_tracking_enabled";

        /// <summary>
        /// The tracking list profile key
        /// </summary>
        private const string TrackingListProfileKey = "packman_tracking_list";

        /// <summary>
        /// Gets the profile.
        /// </summary>
        /// <value>
        /// The profile.
        /// </value>
        private static UserProfile Profile
        {
            get
            {
                return Sitecore.Context.User.Profile;
            }
        }

        /// <summary>
        /// Starts the tracking.
        /// </summary>
        public virtual void StartTracking()
        {
            this.SetCustomProperty(TrackingEnabledProfileKey, true.ToString());
        }

        /// <summary>
        /// Stops the tracking.
        /// </summary>
        public virtual void StopTracking()
        {
            this.SetCustomProperty(TrackingEnabledProfileKey, false.ToString());
        }

        /// <summary>
        /// Saves the tracking list.
        /// </summary>
        /// <param name="data">The data.</param>
        public virtual void SaveTrackingData(string data)
        {
            this.SetCustomProperty(TrackingListProfileKey, data);
        }

        /// <summary>
        /// Gets the tracking list.
        /// </summary>
        /// <returns>
        /// The track list
        /// </returns>
        public virtual string GetTrackingData()
        {
            return this.GetCustomProperty(TrackingListProfileKey);
        }

        /// <summary>
        /// Determines whether the tracking is enabled.
        /// </summary>
        /// <returns>
        /// Boolean value whether tracking is enabled
        /// </returns>
        public virtual bool IsTrackingEnabled()
        {
            return this.GetCustomProperty(TrackingEnabledProfileKey) == true.ToString();
        }

        /// <summary>
        /// Gets the custom property.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The custom property value</returns>
        private string GetCustomProperty(string name)
        {
            return Profile.GetCustomProperty(name);
        }

        /// <summary>
        /// Sets the custom property.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        private void SetCustomProperty(string name, string value)
        {
            Profile.SetCustomProperty(name, value);
            Profile.Save();
        }
    }
}
