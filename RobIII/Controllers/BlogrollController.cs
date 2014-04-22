using PagedList;
using RobIII.Models;
using System.Web;
using System.Web.Mvc;

namespace RobIII.Controllers
{
    [RoutePrefix("blogroll")]
    public class BlogrollController : Controller
    {
        [Route]
        [Route("~/")]
        [Route("{language?}/{page:int?}/{pagesize:int?}", Name = "Blogroll")]
        public ActionResult Index(string language = null, int page = 1, int pageSize = 5)
        {
            language = language ?? "all";
            var model = new BlogrollViewmodel
            {
                PagedList = new APIController().GetFeeds(language).ToPagedList(page, pageSize),
                Language = language
            };

            if (model.PagedList.Count == 0)
                throw new HttpException(404, "No items for this page");

            return View(model);
        }
    }
}