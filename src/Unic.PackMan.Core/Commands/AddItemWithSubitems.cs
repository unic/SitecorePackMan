namespace Unic.PackMan.Core.Commands
{
    using System.Linq;
    using Pipelines.AddItem;
    using Sitecore.Diagnostics;
    using Sitecore.Pipelines;
    using Sitecore.Shell.Framework.Commands;
    using Unic.PackMan.Core.User;

    public class AddItemWithSubitems : Command
    {
        /// <summary>
        /// The user service
        /// </summary>
        private readonly IUserService userService;

        public AddItemWithSubitems() : this(new UserService())
        {
        }

        public AddItemWithSubitems(IUserService userService)
        {
            this.userService = userService;
        }
        
        public override void Execute(CommandContext context)
        {
            var item = context.Items.FirstOrDefault();
            Assert.IsNotNull(item, "Item must not be null");

            var pipelineArgs = new AddItemPipelineArgs { Item = item, AddWithSubItems = true };
            CorePipeline.Run("PackMan.AddItem", pipelineArgs);

            Sitecore.Context.ClientPage.SendMessage(this, "item:refresh");
        }

        public override CommandState QueryState(CommandContext context)
        {
            if (!this.userService.IsTrackingEnabled())
            {
                return CommandState.Disabled;
            }

            return base.QueryState(context);
        }
    }
}
