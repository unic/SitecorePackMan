namespace Unic.PackMan.Core.Tracking
{
    using Sitecore;
    using Sitecore.Data;
    using Sitecore.Diagnostics;
    using Sitecore.Globalization;
    using Sitecore.Install.Items;
    using Sitecore.Resources;
    using Sitecore.Shell.Applications.ContentManager.Galleries;
    using Sitecore.Web.UI.HtmlControls;
    using Sitecore.Web.UI.Sheer;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.UI;
    using Unic.PackMan.Core.DependencyInjection;

    /// <summary>
    /// Code behind of the view that shows currently tracked items.
    /// </summary>
    public class TrackedItemsForm : GalleryForm
    {
        /// <summary>
        /// The tracking service
        /// </summary>
        private readonly ITrackingService trackingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackedItemsForm"/> class.
        /// </summary>
        public TrackedItemsForm()
        {
            this.trackingService = ContainerFactory.Resolve<ITrackingService>();
        }

        /// <summary>
        /// Gets or sets the links.
        /// </summary>
        /// <value>
        /// The links.
        /// </value>
        protected Border Links { get; set; }

        /// <summary>
        /// Handles the message.
        /// </summary>
        /// <param name="message">The message.</param>
        public override void HandleMessage(Message message)
        {
            Assert.ArgumentNotNull(message, "message");
            this.Invoke(message, true);
            message.CancelBubble = true;
            message.CancelDispatch = true;
        }

        /// <summary>
        /// Raises the <see cref="E:Load" /> event.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected override void OnLoad(EventArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            base.OnLoad(args);

            if (Context.ClientPage.IsEvent) return;

            var result = new StringBuilder();
            var tracking = this.trackingService.GetTrackingData();
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

        /// <summary>
        /// Renders the tracking.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="items">The items.</param>
        /// <param name="title">The title.</param>
        /// <param name="icon">The icon.</param>
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
