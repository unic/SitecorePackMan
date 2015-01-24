using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unic.PackMan.Core.Pipelines.RemoveItem
{
    using Sitecore.Data.Items;
    using Sitecore.Pipelines;

    public class RemoveItemPipelineArgs : PipelineArgs
    {
        public Item Item { get; set; }
    }
}
