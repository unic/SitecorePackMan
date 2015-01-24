namespace Unic.PackMan.Core.Pipelines.StartTracking
{
    using Sitecore.Pipelines;
    using Unic.PackMan.Core.User;

    public class StartTracking
    {
        private readonly IUserService userService;

        public StartTracking(IUserService userService)
        {
            this.userService = userService;
        }

        public void Process(PipelineArgs args)
        {
            this.userService.StartTracking();
        }
    }
}
