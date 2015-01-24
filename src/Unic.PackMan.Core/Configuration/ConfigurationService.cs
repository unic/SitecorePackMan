namespace Unic.PackMan.Core.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Sitecore.Configuration;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Data.Serialization.Presets;
    using Sitecore.Diagnostics;

    /// <summary>
    /// Service for configuration logic. Thanks to Kamsar for some hints and tips (https://github.com/kamsar/Unicorn/blob/master/src/Unicorn/Predicates/SerializationPresetPredicate.cs). !!
    /// </summary>
    public class ConfigurationService : IConfigurationService
    {
        /// <summary>
        /// The preset
        /// </summary>
        private readonly IEnumerable<IncludeEntry> preset = Enumerable.Empty<IncludeEntry>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationService"/> class.
        /// </summary>
        public ConfigurationService()
        {
            var config = Factory.GetConfigNode("packman");
            if (config == null)
            {
                Log.Info("No include/exclude configuration found. Everything will be included.", this);
                return;
            }

            this.preset = PresetFactory.Create(config);
        }

        /// <summary>
        /// Determines whether an item is included/excluded by the configuration.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// Whether the item is included
        /// </returns>
        public virtual bool IsItemIncluded(Item item)
        {
            var result = true;
            foreach (var entry in this.preset)
            {
                result = this.Includes(entry, item);
                if (result) return true;
            }

            return result;
        }

        /// <summary>
        /// Checks if a preset includes a given item
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <param name="item">The item.</param>
        /// <returns>Whether item is included</returns>
        protected virtual bool Includes(IncludeEntry entry, Item item)
        {
            // check for db match 
            if (item.Database.Name != entry.Database) return false;

            // check for path match
            if (!item.Paths.FullPath.StartsWith(entry.Path, StringComparison.OrdinalIgnoreCase)) return false;

            // check excludes
            return this.ExcludeMatches(entry, item);
        }

        /// <summary>
        /// Checks if a preset excludes a given item
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <param name="item">The item.</param>
        /// <returns>Whether the item matches exclude rules</returns>
        protected virtual bool ExcludeMatches(IncludeEntry entry, Item item)
        {
            var result = this.ExcludeMatchesTemplate(entry.Exclude, item.TemplateName);
            if (!result) return false;

            result = this.ExcludeMatchesTemplateId(entry.Exclude, item.TemplateID);
            if (!result) return false;

            result = this.ExcludeMatchesPath(entry.Exclude, item.Paths.FullPath);
            if (!result) return false;

            result = this.ExcludeMatchesId(entry.Exclude, item.ID);
            return result;
        }

        /// <summary>
        /// Checks if a given list of excludes matches a specific Serialization path
        /// </summary>
        /// <param name="entries">The entries.</param>
        /// <param name="sitecorePath">The sitecore path.</param>
        /// <returns>Whether path matches an exclude config</returns>
        protected virtual bool ExcludeMatchesPath(IEnumerable<ExcludeEntry> entries, string sitecorePath)
        {
            return !entries.Any(entry => entry.Type.Equals("path", StringComparison.Ordinal) && sitecorePath.StartsWith(entry.Value, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Checks if a given list of excludes matches a specific item id.
        /// </summary>
        /// <param name="entries">The entries.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>Whether the id matches an exclude config</returns>
        protected virtual bool ExcludeMatchesId(IEnumerable<ExcludeEntry> entries, ID id)
        {
            return !entries.Any(entry => entry.Type.Equals("id", StringComparison.Ordinal) && entry.Value.Equals(id.ToString(), StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Checks if a given list of excludes matches a specific template name
        /// </summary>
        /// <param name="entries">The entries.</param>
        /// <param name="templateName">Name of the template.</param>
        /// <returns>Whether the template name matches an exclude config</returns>
        protected virtual bool ExcludeMatchesTemplate(IEnumerable<ExcludeEntry> entries, string templateName)
        {
            return !entries.Any(entry => entry.Type.Equals("template", StringComparison.Ordinal) && entry.Value.Equals(templateName, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Checks if a given list of excludes matches a specific template ID
        /// </summary>
        /// <param name="entries">The entries.</param>
        /// <param name="templateId">The template identifier.</param>
        /// <returns>Whether the template id matches an exclude config</returns>
        protected virtual bool ExcludeMatchesTemplateId(IEnumerable<ExcludeEntry> entries, ID templateId)
        {
            return !entries.Any(entry => entry.Type.Equals("templateid", StringComparison.Ordinal) && entry.Value.Equals(templateId.ToString(), StringComparison.OrdinalIgnoreCase));
        }
    }
}
