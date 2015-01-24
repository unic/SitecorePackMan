using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unic.PackMan.Core.Pipelines.AddItem
{
    using Sitecore.Data.Items;
    using Sitecore.Pipelines;

    public class AddItemPipelineArgs : PipelineArgs
    {
        public Item Item { get; set; }

        public bool AddWithSubItems { get; set; }
    }
}
