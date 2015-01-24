namespace Unic.PackMan.Core.User
{
    using Sitecore.Diagnostics;
    using Sitecore.Security;

    public class UserService : IUserService
    {
        private const string TrackingEnabledProfileKey = "packman_tracking_enabled";

        private const string TrackingListProfileKey = "packman_tracking_list";
        
        private static UserProfile Profile
        {
            get
            {
                return Sitecore.Context.User.Profile;
            }
        }
        
        public virtual void StartTracking()
        {
            this.SetCustomProperty(TrackingEnabledProfileKey, true.ToString());
        }

        public virtual void StopTracking()
        {
            this.SetCustomProperty(TrackingEnabledProfileKey, false.ToString());
        }

        public void SaveTrackingList(string data)
        {
            this.SetCustomProperty(TrackingListProfileKey, data);
        }

        public string GetTrackingList()
        {
            return this.GetCustomProperty(TrackingListProfileKey);
        }

        public bool IsTrackingEnabled()
        {
            return this.GetCustomProperty(TrackingEnabledProfileKey) == true.ToString();
        }

        private string GetCustomProperty(string name)
        {
            return Profile.GetCustomProperty(name);
        }

        private void SetCustomProperty(string name, string value)
        {
            Profile.SetCustomProperty(name, value);
            Profile.Save();
        }
    }
}
