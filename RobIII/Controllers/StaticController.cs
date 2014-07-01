using System.Web.Mvc;
using System.Web.Routing;

namespace RobIII.Controllers
{
    [RoutePrefix("static")]
    public class StaticController : Controller
    {
        [Route("{page:static}", Name = "Static")]
        public ActionResult Index(string page)
        {
            return View(string.Format("~/Views/Static/{0}.cshtml", page));
        }   
    }
}