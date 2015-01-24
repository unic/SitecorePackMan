namespace Unic.PackMan.Core.Commands
{
    using Sitecore.Data;
    using Sitecore.Globalization;
    using Sitecore.Pipelines;
    using Sitecore.Shell.Framework.Commands;
    using Sitecore.Web.UI.Sheer;
    using System.Collections.Specialized;

    /// <summary>
    /// Command to stop a tracking session
    /// </summary>
    public class StopTracking : CommandBase
    {
        /// <summary>
        /// Executes the command
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Execute(CommandContext context)
        {
            Sitecore.Context.ClientPage.Start(this, "Run", new NameValueCollection());
        }

        /// <summary>
        /// Runs the short URL generation.
        /// </summary>
        /// <param name="args">The client pipeline arguments.</param>
        protected void Run(ClientPipelineArgs args)
        {
            if (!args.IsPostBack)
            {
                SheerResponse.Confirm(Translate.Text("Are you sure you want to stop tracking? Tracked items will be lost. If you don't want to loose your tracked items, please generate the package first"));
                args.WaitForPostBack();
            }
            else if (args.Result == "yes")
            {
                var trackingItems = this.TrackingService.GetTrackingData();

                CorePipeline.Run("PackMan.StopTracking", new PipelineArgs());
                this.RefreshItem();

                if (trackingItems == null) return;
                foreach (var item in trackingItems.Items)
                {
                    var message = Message.Parse(this, "item:updategutter");
                    message.Arguments.Add("id", new ItemUri(item.Uri).ItemID.ToString());
                    Sitecore.Context.ClientPage.SendMessage(message);
                }
            }
        }
    }
}
