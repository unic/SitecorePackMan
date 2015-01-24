using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unic.PackMan.Core.Pipelines.RemoveItem
{
    using Unic.PackMan.Core.Pipelines.AddItem;
    using Unic.PackMan.Core.Tracking;

    public class RemoveItemFromTrack
    {
        private readonly ITrackingService trackingService;

        public RemoveItemFromTrack(ITrackingService trackingService)
        {
            this.trackingService = trackingService;
        }

        public void Process(RemoveItemPipelineArgs args)
        {
            this.trackingService.RemoveItemFromTrack(args.Item);
        }
    }
}
