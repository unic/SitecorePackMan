namespace Unic.PackMan.Core.User
{
    using Sitecore.Security;

    public class UserService : IUserService
    {
        private const string TrackingEnabledProfileKey = "packman_tracking_enabled";
        
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
        }
    }
}
