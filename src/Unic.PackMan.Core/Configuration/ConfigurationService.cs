namespace Unic.PackMan.Core.Configuration
{
    using Sitecore.Data.Items;

    public class ConfigurationService : IConfigurationService
    {
        public bool IsItemIncluded(Item item)
        {
            return true;
        }
    }
}
