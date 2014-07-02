using RobIII.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RobIII.Helpers
{
    public static class ModelExtensions
    {
        public static string IsCurrentLanguage(this BlogrollViewmodel model, FeedLanguage language)
        {
            return IsCurrentLanguage(model, language.ToString());
        }

        public static string IsCurrentLanguage(this BlogrollViewmodel model, string language)
        {
            return model.Language.Equals(language, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }

        public static string GetLanguageString(this BlogrollViewmodel model, string defaultString = null)
        {
            switch (model.Language.ToLowerInvariant())
            {
                case "en":
                    return "English";
                case "nl":
                    return "Nederlands";
                default:
                    return defaultString;
            }
        }

        public static string IsCurrentStatus(this PocketViewmodel model, PocketStatus status)
        {
            return IsCurrentStatus(model, status.ToString());
        }

        public static string IsCurrentStatus(this PocketViewmodel model, string status)
        {
            return model.Status.Equals(status, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}