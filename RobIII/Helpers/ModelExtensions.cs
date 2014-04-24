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

    }
}