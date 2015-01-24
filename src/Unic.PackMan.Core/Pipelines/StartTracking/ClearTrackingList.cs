namespace Unic.PackMan.Core.Pipelines.StartTracking
{
    using Sitecore.Pipelines;
    using Unic.PackMan.Core.User;

    public class ClearTrackingList
    {
        private readonly IUserService userService;

        public ClearTrackingList(IUserService userService)
        {
            this.userService = userService;
        }

        public void Process(PipelineArgs args)
        {
            this.userService.SaveTrackingList(string.Empty);
        }
    }
}
