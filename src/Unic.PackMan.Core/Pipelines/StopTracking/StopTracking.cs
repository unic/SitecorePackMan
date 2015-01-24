namespace Unic.PackMan.Core.Pipelines.StopTracking
{
    using Sitecore.Pipelines;
    using Unic.PackMan.Core.User;

    public class StopTracking
    {
        private readonly IUserService userService;

        public StopTracking(IUserService userService)
        {
            this.userService = userService;
        }

        public void Process(PipelineArgs args)
        {
            this.userService.StopTracking();
        }
    }
}
