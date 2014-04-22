using RobIII.Helpers;
using SimpleFeedReader;
using System.Linq;
using System.Web.Http;

namespace RobIII.Controllers
{
    [RoutePrefix("api")]
    public class APIController : ApiController
    {
        [Route("blogroll/{language}/{page:int?}/{pagesize:int?}")]
        public IQueryable<FeedItem> GetFeeds(string language, int page = 1, int pageSize  = 10)
        {
            return this.GetFeeds(language).Skip((page - 1) * pageSize).Take(pageSize);

        }
        public IQueryable<FeedItem> GetFeeds(string language)
        {
            return new FeedRetriever().GetByLanguage(language);
        }
    }
}
