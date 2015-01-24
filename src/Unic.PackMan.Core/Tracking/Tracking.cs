namespace Unic.PackMan.Core.Tracking
{
    using System.Collections.Generic;

    public class Tracking
    {
        public Tracking()
        {
            this.Items = new List<TrackedItem>();
        }
        
        public IList<TrackedItem> Items { get; private set; } 
    }
}
