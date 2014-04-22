using RobIII.Helpers;
using SimpleFeedReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Web;
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

        public IEnumerable<FeedItem> GetFeeds(FeedLanguage language = FeedLanguage.All, int limit = 10)
        {
            return new FeedRetriever()
                .RetrieveFeeds(FeedRetriever.Feeds.Where(f => language == FeedLanguage.All || f.Language == language))
                .OrderByDescending(i => i.Date)
                .Take(limit);
        }
    }
}