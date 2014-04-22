using SimpleFeedReader;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace RobIII.Helpers
{
    public enum FeedLanguage
    {
        Unknown,
        English,
        Dutch,
        All
    }

    public class Feed
    {
        public string Uri { get; set; }
        public FeedLanguage Language { get; set; }
    }

    public class FeedRetriever
    {

        public TimeSpan DefaultTTL { get; set; }

        //TODO: Move to config or something
        public static readonly List<Feed> Feeds = new List<Feed>(
            new[] {
                new Feed { Language = FeedLanguage.Dutch, Uri = "http://robiii.tweakblogs.net/feed/" },
                new Feed { Language = FeedLanguage.English, Uri = "http://blog.robiii.nl/feeds/posts/default?alt=rss" },
            }
        );

        public FeedRetriever()
            : this(TimeSpan.FromSeconds(int.Parse(ConfigurationManager.AppSettings["DefaultFeedTTL"]))) { }

        public FeedRetriever(TimeSpan defaultTTL)
        {
            this.DefaultTTL = defaultTTL;
        }

        public IEnumerable<FeedItem> RetrieveFeeds(IEnumerable<Feed> feeds)
        {
            return this.RetrieveFeeds(feeds, this.DefaultTTL);
        }

        public IEnumerable<FeedItem> RetrieveFeeds(IEnumerable<Feed> feeds, TimeSpan ttl)
        {
            var items = new List<FeedItem>();
            foreach (var f in feeds)
            {
                items.AddRange(
                    HttpRuntime.Cache.GetOrStore<IEnumerable<FeedItem>>(string.Format("feed_{0}_{1}", f.Language, f.Uri.ToLowerInvariant()), () =>
                    {
                        var reader = new FeedReader(new RobIIIFeedNormalizer());
                        return reader.RetrieveFeed(f.Uri);
                    }, ttl)
                );
            }

            return items;
        }
    }
}