namespace Unic.PackMan.Core.Commands
{
    using System.Linq;
    using Pipelines.RemoveItem;
    using Sitecore.Diagnostics;
    using Sitecore.Pipelines;
    using Sitecore.Shell.Framework.Commands;
    using Unic.PackMan.Core.Configuration;
    using Unic.PackMan.Core.Tracking;
    using Unic.PackMan.Core.User;

    public class RemoveItem : Command
    {
        /// <summary>
        /// The user service
        /// </summary>
        private readonly IUserService userService;

        private readonly ITrackingService trackingService;

        public RemoveItem()
            : this(new TrackingService(new UserService(), new ConfigurationService()), new UserService())
        {
        }

        public RemoveItem(ITrackingService trackingService, IUserService userService)
        {
            this.trackingService = trackingService;
            this.userService = userService;
        }

        public override void Execute(CommandContext context)
        {
            var item = context.Items.FirstOrDefault();
            Assert.IsNotNull(item, "Item must not be null");

            var pipelineArgs = new RemoveItemPipelineArgs {Item = item};
            CorePipeline.Run("PackMan.RemoveItem", pipelineArgs);

            Sitecore.Context.ClientPage.SendMessage(this, "item:refresh");
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
