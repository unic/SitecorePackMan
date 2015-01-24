namespace Unic.PackMan.Core.Pipelines.GeneratePackage
{
    using System.Collections.Generic;
    using Sitecore.Data;
    using Sitecore.Pipelines;

    /// <summary>
    /// The pipeline arguments of the GeneratePackage pipeline
    /// </summary>
    public class GeneratePackagePipelineArgs : PipelineArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneratePackagePipelineArgs"/> class.
        /// </summary>
        public GeneratePackagePipelineArgs()
        {
            this.PackageItems = new List<ItemUri>();
        }

        /// <summary>
        /// Gets or sets the download path.
        /// </summary>
        /// <value>
        /// The download path.
        /// </value>
        public string DownloadPath { get; set; }

        /// <summary>
        /// Gets or sets the name of the package.
        /// </summary>
        /// <value>
        /// The name of the package.
        /// </value>
        public string PackageName { get; set; }

        /// <summary>
        /// Gets or sets the package author.
        /// </summary>
        /// <value>
        /// The package author.
        /// </value>
        public string PackageAuthor { get; set; }

        /// <summary>
        /// Gets the package items.
        /// </summary>
        /// <value>
        /// The package items.
        /// </value>
        public IList<ItemUri> PackageItems { get; private set; }
    }
}
