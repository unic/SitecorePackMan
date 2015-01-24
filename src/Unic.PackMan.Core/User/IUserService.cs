namespace Unic.PackMan.Core.User
{
    /// <summary>
    /// Service for user logic.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Starts the tracking.
        /// </summary>
        void StartTracking();

        /// <summary>
        /// Stops the tracking.
        /// </summary>
        void StopTracking();

        /// <summary>
        /// Saves the tracking list.
        /// </summary>
        /// <param name="data">The data.</param>
        void SaveTrackingData(string data);

        /// <summary>
        /// Gets the tracking list.
        /// </summary>
        /// <returns>The track list</returns>
        string GetTrackingData();

        /// <summary>
        /// Determines whether the tracking is enabled.
        /// </summary>
        /// <returns>Boolean value whether tracking is enabled</returns>
        bool IsTrackingEnabled();
    }
}
