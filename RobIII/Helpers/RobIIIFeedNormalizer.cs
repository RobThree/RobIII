using SimpleFeedReader;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;

namespace RobIII.Helpers
{
    internal class RobIIIFeedNormalizer : DefaultFeedItemNormalizer, IFeedItemNormalizer
    {
        private readonly Regex _readMore = new Regex(@"((?:Lees verder|Read More)\s+(?:»|&raquo;))$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
        public override FeedItem Normalize(SyndicationFeed feed, SyndicationItem item)
        {
            var i = base.Normalize(feed, item);

            if (!string.IsNullOrEmpty(i.Summary))
            {
                i.Summary = _readMore.Replace(i.Summary, string.Empty).Trim();
            }

            if (!string.IsNullOrEmpty(i.Content))
            {
                i.Content = _readMore.Replace(i.Content, string.Empty).Trim();
            }

            return i;
        }
    }
}