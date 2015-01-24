namespace Unic.PackMan.Core.Pipelines.StartTracking
{
    using Sitecore.Pipelines;
    using Unic.PackMan.Core.User;

    /// <summary>
    /// Clears the tracking list pipeline processor.
    /// </summary>
    public class ClearTrackingList
    {
        /// <summary>
        /// The user service
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClearTrackingList"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        public ClearTrackingList(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// Processes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public void Process(PipelineArgs args)
        {
            this.userService.SaveTrackingList(string.Empty);
        }
    }
}
