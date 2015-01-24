using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unic.PackMan.Core.Pipelines.AddItem
{
    using Unic.PackMan.Core.Tracking;

    public class AddItemToTrack
    {
        private readonly ITrackingService trackingService;

        public AddItemToTrack(ITrackingService trackingService)
        {
            this.trackingService = trackingService;
        }

        public void Process(AddItemPipelineArgs args)
        {
            this.trackingService.AddItemToTrack(args.Item, args.AddWithSubItems);
        }
    }
}
