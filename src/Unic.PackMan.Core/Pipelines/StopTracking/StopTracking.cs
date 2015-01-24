namespace Unic.PackMan.Core.Pipelines.StopTracking
{
    using Sitecore.Pipelines;
    using Unic.PackMan.Core.User;

    /// <summary>
    /// Stops the tracking pipeline processor
    /// </summary>
    public class StopTracking
    {
        /// <summary>
        /// The user service
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="StopTracking"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        public StopTracking(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// Processes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public void Process(PipelineArgs args)
        {
            this.userService.StopTracking();
        }
    }
}
