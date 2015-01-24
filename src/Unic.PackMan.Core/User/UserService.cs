namespace Unic.PackMan.Core.User
{
    public class UserService : IUserService
    {
        public void StartTracking()
        {
            Sitecore.Diagnostics.Log.Error("START !!!!!!!", this);
        }

        public void StopTracking()
        {
            Sitecore.Diagnostics.Log.Error("STOP !!!!!!!", this);
        }
    }
}
