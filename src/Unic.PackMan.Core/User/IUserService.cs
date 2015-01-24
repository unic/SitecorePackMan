namespace Unic.PackMan.Core.User
{
    public interface IUserService
    {
        void StartTracking();

        void StopTracking();

        bool IsTrackingEnabled();
    }
}
