using PagedList;
using RobIII.Models;
using System.Web;
using System.Web.Mvc;

namespace RobIII.Controllers
{
    [RoutePrefix("pocket")]
    public class PocketController : Controller
    {
        [Route("{status?}/{page:int?}/{pagesize:int?}", Name = "Pocket")]
        public ActionResult Index(string status = null, int page = 1, int pageSize = 10, string search = null)
        {
            status = status ?? "All";
            var model = new PocketViewmodel
            {
                PagedList = new APIController().GetPocket(status, search).ToPagedList(page, pageSize),
                Status = status,
                Search = search
            };

            return View(model);
        }
    }
}