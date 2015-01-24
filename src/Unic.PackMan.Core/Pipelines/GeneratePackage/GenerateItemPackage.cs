namespace Unic.PackMan.Core.Pipelines.GeneratePackage
{
    using System.Linq;
    using Unic.PackMan.Core.PackageGeneration;

    /// <summary>
    /// Generates the item package
    /// </summary>
    public class GenerateItemPackage
    {
        /// <summary>
        /// The package generation service
        /// </summary>
        private readonly IPackageGenerationService packageGenerationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateItemPackage"/> class.
        /// </summary>
        /// <param name="packageGenerationService">The package generation service.</param>
        public GenerateItemPackage(IPackageGenerationService packageGenerationService)
        {
            this.packageGenerationService = packageGenerationService;
        }

        /// <summary>
        /// Generates the package
        /// </summary>
        /// <param name="args">The arguments.</param>
        public void Process(GeneratePackagePipelineArgs args)
        {
            if (!args.PackageItems.Any()) return;

            string downloadPath;
            if (this.packageGenerationService.GeneratePackage(
                args.PackageItems,
                args.PackageName,
                args.PackageAuthor,
                out downloadPath))
            {
                args.DownloadPath = downloadPath;
            }
        }
    }
}
