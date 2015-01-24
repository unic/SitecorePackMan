namespace Unic.PackMan.Core.Pipelines.StartTracking
{
    using Sitecore.Pipelines;
    using Unic.PackMan.Core.User;

    /// <summary>
    /// Starts the tracking pipeline processor
    /// </summary>
    public class StartTracking
    {
        /// <summary>
        /// The user service
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="StartTracking"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        public StartTracking(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// Processes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public void Process(PipelineArgs args)
        {
            this.userService.StartTracking();
        }
    }
}
