namespace Unic.PackMan.Core.Tracking
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Web.UI;
    using Configuration;
    using Sitecore;
    using Sitecore.Diagnostics;
    using Sitecore.Globalization;
    using Sitecore.Resources;
    using Sitecore.Shell.Applications.ContentManager.Galleries;
    using Sitecore.Web.UI.HtmlControls;
    using Sitecore.Web.UI.Sheer;
    using User;

    public class TrackedItemsForm : GalleryForm
    {
        protected Border Links;

        private readonly ITrackingService trackingService;

        public TrackedItemsForm()
            : this(new TrackingService(new UserService(), new ConfigurationService()))
        {
        }

        public TrackedItemsForm(ITrackingService trackingService)
        {
            this.trackingService = trackingService;
        }
        public override void HandleMessage(Message message)
        {
            Assert.ArgumentNotNull((object)message, "message");
            this.Invoke(message, true);
            message.CancelBubble = true;
            message.CancelDispatch = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            Assert.ArgumentNotNull(e, "e");
            base.OnLoad(e);

            if (Context.ClientPage.IsEvent) return;

            var result = new StringBuilder();
            var tracking = this.trackingService.GetTracking();
            if (tracking.Items.Any())
            {
                this.RenderTracking(result, tracking);
            }
            if (result.Length == 0)
            {
                result.Append(Translate.Text("There are no items tracked."));
            }
            this.Links.Controls.Add(new LiteralControl(result.ToString()));
        }
        
        private void RenderTracking(StringBuilder result, Tracking tracking)
        {
            result.Append("<div style=\"font-weight:bold;padding:2px 0px 4px 0px\">" + Translate.Text("Tracked Items:") + "</div>");
            foreach (var trackingItem in tracking.Items)
            {
                var icon = trackingItem.WithSubItems ? "Office/32x32/arrow_fork.png" : "Office/32x32/arrow_right.png";
                result.Append("<a href=\"#\" class=\"scLink\" >" + Images.GetImage(icon, 16, 16, "absmiddle", "0px 4px 0px 0px")  + trackingItem.Uri + "</a>");
                //result.Append("<a href=\"#\" class=\"scLink\" onclick='javascript:return scForm.invoke(\"item:load(id=" + trackingItem.Uri + ")\")'>" + Images.GetImage(part1.Appearance.Icon, 16, 16, "absmiddle", "0px 4px 0px 0px") + part1.DisplayName + " - [" + part1.Paths.Path + "]</a>");
            }
        }
    }
}
