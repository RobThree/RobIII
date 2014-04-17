using SimpleFeedReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace RobIII.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/
        public ActionResult Index()
        {
            var model = this.GetFeeds();
            return View(model);
        }

        public IEnumerable<FeedItem> GetFeeds(int limit = 10)
        {
            var items = HttpRuntime.Cache.GetOrStore<IEnumerable<FeedItem>>("items", () =>
            {
                var f = new FeedReader(new RobIIIFeedNormalizer());
                return f.RetrieveFeeds(new[] { 
                    "http://blog.robiii.nl/feeds/posts/default?alt=rss",
                    "http://robiii.tweakblogs.net/feed/"
                });
            }, TimeSpan.FromMinutes(10));

            
            return items.Take(limit).OrderByDescending(i => i.Date);
        }

        private class RobIIIFeedNormalizer : DefaultFeedItemNormalizer, IFeedItemNormalizer
        {
            private Regex _readMore = new Regex(@"((?:Lees verder|Read More)\s+(?:»|&raquo;))$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
            public override FeedItem Normalize(SyndicationFeed feed, SyndicationItem item)
            {
                var i = base.Normalize(feed, item);

                if (!string.IsNullOrEmpty(i.Summary))
                    i.Summary = _readMore.Replace(i.Summary, string.Empty).Trim();

                if (!string.IsNullOrEmpty(i.Content))
                    i.Content = _readMore.Replace(i.Content, string.Empty).Trim();
                return i;
            }
        }
    }
}

//TODO: Move elsewhere
public static class CacheExtensions
{
    public static readonly TimeSpan DefaultCacheExpiration = TimeSpan.FromMinutes(20);

    /// <summary>
    /// Allows Caching of typed data
    /// </summary>
    /// <example><![CDATA[
    /// var user = HttpRuntime
    ///   .Cache
    ///   .GetOrStore<User>(
    ///      string.Format("User{0}", _userId), 
    ///      () => Repository.GetUser(_userId));
    ///
    /// ]]></example>
    /// <typeparam name="T"></typeparam>
    /// <param name="cache">calling object</param>
    /// <param name="key">Cache key</param>
    /// <param name="generator">Func that returns the object to store in cache</param>
    /// <returns></returns>
    /// <remarks>Uses a default cache expiration period as defined in <see cref="CacheExtensions.DefaultCacheExpiration"/></remarks>
    public static T GetOrStore<T>(this Cache cache, string key, Func<T> generator)
    {
        return cache.GetOrStore(key, (cache[key] == null && generator != null) ? generator() : default(T), DefaultCacheExpiration);
    }


    /// <summary>
    /// Allows Caching of typed data
    /// </summary>
    /// <example><![CDATA[
    /// var user = HttpRuntime
    ///   .Cache
    ///   .GetOrStore<User>(
    ///      string.Format("User{0}", _userId), 
    ///      () => Repository.GetUser(_userId));
    ///
    /// ]]></example>
    /// <typeparam name="T"></typeparam>
    /// <param name="cache">calling object</param>
    /// <param name="key">Cache key</param>
    /// <param name="generator">Func that returns the object to store in cache</param>
    /// <param name="ttl">Time to expire cache</param>
    /// <returns></returns>
    public static T GetOrStore<T>(this Cache cache, string key, Func<T> generator, TimeSpan ttl)
    {
        return cache.GetOrStore(key, (cache[key] == null && generator != null) ? generator() : default(T), ttl);
    }


    /// <summary>
    /// Allows Caching of typed data
    /// </summary>
    /// <example><![CDATA[
    /// var user = HttpRuntime
    ///   .Cache
    ///   .GetOrStore<User>(
    ///      string.Format("User{0}", _userId),_userId));
    ///
    /// ]]></example>
    /// <typeparam name="T"></typeparam>
    /// <param name="cache">calling object</param>
    /// <param name="key">Cache key</param>
    /// <param name="obj">Object to store in cache</param>
    /// <returns></returns>
    /// <remarks>Uses a default cache expiration period as defined in <see cref="CacheExtensions.DefaultCacheExpiration"/></remarks>
    public static T GetOrStore<T>(this Cache cache, string key, T obj)
    {
        return cache.GetOrStore(key, obj, DefaultCacheExpiration);
    }

    /// <summary>
    /// Allows Caching of typed data
    /// </summary>
    /// <example><![CDATA[
    /// var user = HttpRuntime
    ///   .Cache
    ///   .GetOrStore<User>(
    ///      string.Format("User{0}", _userId), 
    ///      () => Repository.GetUser(_userId));
    ///
    /// ]]></example>
    /// <typeparam name="T"></typeparam>
    /// <param name="cache">calling object</param>
    /// <param name="key">Cache key</param>
    /// <param name="obj">Object to store in cache</param>
    /// <param name="ttl">Time to expire cache</param>
    /// <returns></returns>
    public static T GetOrStore<T>(this Cache cache, string key, T obj, TimeSpan ttl)
    {
        var result = cache[key];

        if (result == null)
        {

            lock (typeof(CacheExtensions))
            {
                if (result == null)
                {
                    result = obj != null ? obj : default(T);
                    cache.Insert(key, result, null, DateTime.Now.Add(ttl), Cache.NoSlidingExpiration);
                }
            }
        }

        return (T)result;
    }
}