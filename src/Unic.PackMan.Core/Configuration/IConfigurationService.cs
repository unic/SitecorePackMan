namespace Unic.PackMan.Core.Configuration
{
    using Sitecore.Data.Items;

    public interface IConfigurationService
    {
        bool IsItemIncluded(Item item);
    }
}
