namespace Unic.PackMan.Core.PackageGeneration
{
    using System.Collections.Generic;
    using Sitecore.Data;
    
    /// <summary>
    /// Interface for the package generation service
    /// </summary>
    public interface IPackageGenerationService
    {
        /// <summary>
        /// Generates the package.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="packageName">Name of the package.</param>
        /// <param name="packageAuthor">The package author.</param>
        /// <param name="downloadPath">The download path.</param>
        /// <returns></returns>
        bool GeneratePackage(IEnumerable<ItemUri> items, string packageName, string packageAuthor, out string downloadPath);
    }
}
