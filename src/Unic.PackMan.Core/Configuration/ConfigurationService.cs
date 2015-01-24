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

    public class ConfigurationService : IConfigurationService
    {
        private readonly IEnumerable<IncludeEntry> preset = Enumerable.Empty<IncludeEntry>();

        public ConfigurationService()
        {
            var config = Factory.GetConfigNode("packman");
            if (config == null)
            {
                Log.Warn("No include/exclude configuration found", this);
                return;
            }

            this.preset = PresetFactory.Create(config);
        }

        public bool IsItemIncluded(Item item)
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
        protected virtual bool Includes(IncludeEntry entry, Item item)
        {
            // check for db match 
            if (item.Database.Name != entry.Database) return false;

            // check for path match
            if (!item.Paths.FullPath.StartsWith(entry.Path, StringComparison.OrdinalIgnoreCase)) return false;

            // check excludes
            return ExcludeMatches(entry, item);
        }

        /// <summary>
        /// Checks if a preset excludes a given item
        /// </summary>
        protected virtual bool ExcludeMatches(IncludeEntry entry, Item item)
        {
            var result = ExcludeMatchesTemplate(entry.Exclude, item.TemplateName);
            if (!result) return false;

            result = ExcludeMatchesTemplateId(entry.Exclude, item.TemplateID);
            if (!result) return false;

            result = ExcludeMatchesPath(entry.Exclude, item.Paths.FullPath);
            if (!result) return false;

            result = ExcludeMatchesId(entry.Exclude, item.ID);
            return result;
        }

        /// <summary>
        /// Checks if a given list of excludes matches a specific Serialization path
        /// </summary>
        protected virtual bool ExcludeMatchesPath(IEnumerable<ExcludeEntry> entries, string sitecorePath)
        {
            return !entries.Any(entry => entry.Type.Equals("path", StringComparison.Ordinal) && sitecorePath.StartsWith(entry.Value, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Checks if a given list of excludes matches a specific item ID. Use ID.ToString() format eg {A9F4...}
        /// </summary>
        protected virtual bool ExcludeMatchesId(IEnumerable<ExcludeEntry> entries, ID id)
        {
            return !entries.Any(entry => entry.Type.Equals("id", StringComparison.Ordinal) && entry.Value.Equals(id.ToString(), StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Checks if a given list of excludes matches a specific template name
        /// </summary>
        protected virtual bool ExcludeMatchesTemplate(IEnumerable<ExcludeEntry> entries, string templateName)
        {
            return !entries.Any(entry => entry.Type.Equals("template", StringComparison.Ordinal) && entry.Value.Equals(templateName, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Checks if a given list of excludes matches a specific template ID
        /// </summary>
        protected virtual bool ExcludeMatchesTemplateId(IEnumerable<ExcludeEntry> entries, ID templateId)
        {
            return !entries.Any(entry => entry.Type.Equals("templateid", StringComparison.Ordinal) && entry.Value.Equals(templateId.ToString(), StringComparison.OrdinalIgnoreCase));
        }
    }
}
