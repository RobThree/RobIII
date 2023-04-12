using RobIII.Models;
using System;

namespace RobIII.Helpers
{
    public static class ModelExtensions
    {
        public static string IsCurrentLanguage(this BlogrollViewmodel model, FeedLanguage language)
            => IsCurrentLanguage(model, language.ToString());

        public static string IsCurrentLanguage(this BlogrollViewmodel model, string language)
            => model.Language.Equals(language, StringComparison.OrdinalIgnoreCase) ? "active" : null;

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
            => IsCurrentStatus(model, status.ToString());

        public static string IsCurrentStatus(this PocketViewmodel model, string status)
            => model.Status.Equals(status, StringComparison.OrdinalIgnoreCase) ? "active" : null;

        public static string GetStatusString(this PocketViewmodel model, string defaultString = null)
        {
            switch (model.Status.ToLowerInvariant())
            {
                case "read":
                    return "Read";
                case "unread":
                    return "Unread";
                default:
                    return defaultString;
            }
        }
    }
}