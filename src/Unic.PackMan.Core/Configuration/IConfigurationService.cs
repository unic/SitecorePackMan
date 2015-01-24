namespace Unic.PackMan.Core.Configuration
{
    using Sitecore.Data.Items;

    /// <summary>
    /// Service for configuration logic
    /// </summary>
    public interface IConfigurationService
    {
        /// <summary>
        /// Determines whether an item is included/excluded by the configuration.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Whether the item is included</returns>
        bool IsItemIncluded(Item item);
    }
}
