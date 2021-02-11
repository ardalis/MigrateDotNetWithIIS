using System.Web.Mvc;

namespace DotNetMvcApp.Controllers.Api
{
    public class StatesController : Controller
    {
        // GET: States
        [Route("api/states")]
        public ActionResult Index()
        {
            return Json(new string[] {
                "DotNetMvcApp",
                "Controllers/Api/StatesController" },
            JsonRequestBehavior.AllowGet);
        }
    }
}