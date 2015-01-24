namespace Unic.PackMan.Core.Tracking
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.UI;
    using Configuration;
    using Sitecore;
    using Sitecore.Data;
    using Sitecore.Diagnostics;
    using Sitecore.Globalization;
    using Sitecore.Install.Items;
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
            if (tracking != null)
            {
                this.RenderTracking(result, tracking.Items.Where(item => !item.WithSubItems), Translate.Text("Tracked Items:"), "Office/32x32/arrow_right.png");
                this.RenderTracking(result, tracking.Items.Where(item => item.WithSubItems), Translate.Text("Tracked Items with Subitems:"), "Office/32x32/arrow_fork.png");
            }
            
            if (result.Length == 0)
            {
                result.Append(Translate.Text("There are no items tracked."));
            }

            this.Links.Controls.Add(new LiteralControl(result.ToString()));
        }

        private void RenderTracking(StringBuilder result, IEnumerable<TrackedItem> items, string title, string icon)
        {
            var itemList = items.ToList();
            if (!itemList.Any()) return;

            result.Append("<div class=\"scMenuHeader\"\">" + Images.GetImage(icon, 16, 16, "absmiddle", "0px 4px 0px 0px") + title + "</div>");

            foreach (var trackingItem in itemList.OrderBy(item => item.Path))
            {
                var item = new ItemReference(new ItemUri(trackingItem.Uri), false);
                result.Append("<a href=\"#\" class=\"scLink\" onclick='javascript:return scForm.invoke(\"item:load(id=" + item.ID + ",language=" + item.Language + ",version=" + item.Version + ")\")'>" + Images.GetImage(trackingItem.Icon, 16, 16, "absmiddle", "0px 4px 0px 0px") + trackingItem.DisplayName + " - [" + trackingItem.Path + "]</a>");
            }
        }
    }
}
