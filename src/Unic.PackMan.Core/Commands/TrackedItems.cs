namespace Unic.PackMan.Core.Commands
{
    using System.Linq;
    using Configuration;
    using Sitecore.Shell.Framework.Commands;
    using Tracking;
    using User;

    public class TrackedItems : Command
    {
        private readonly IUserService userService;
        private readonly ITrackingService trackingService;

        public TrackedItems() : this(new UserService(), new TrackingService(new UserService(), new ConfigurationService()))
        {
        }

        public TrackedItems(IUserService userService, ITrackingService trackingService)
        {
            this.userService = userService;
            this.trackingService = trackingService;
        }

        public override void Execute(CommandContext context)
        {
        }

        public override CommandState QueryState(CommandContext context)
        {
            if (!this.userService.IsTrackingEnabled())
            {
                return CommandState.Disabled;
            }

            if (!this.trackingService.IsItemTracked(context.Items.FirstOrDefault()))
            {
                return CommandState.Disabled;
            }

            return base.QueryState(context);
        }
    }
}
