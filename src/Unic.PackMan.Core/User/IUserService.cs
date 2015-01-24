namespace Unic.PackMan.Core.User
{
    public interface IUserService
    {
        void StartTracking();

        void StopTracking();

        void SaveTrackingList(string data);

        string GetTrackingList();

        bool IsTrackingEnabled();
    }
}
