using System.Web.Mvc;

namespace DotNetMvcApp.Controllers.Api
{
    public class ValuesController : Controller
    {
        // GET: Values
        [Route("api/values")]
        public ActionResult Index()
        {
            return Json(new string[] {
                "DotNetMvcApp",
                "Controllers/Api/ValuesController" },
            JsonRequestBehavior.AllowGet);
        }
    }
}