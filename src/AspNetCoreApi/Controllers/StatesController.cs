using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AspNetCoreApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StatesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "AspNetCoreApi running .NET 4.x", "AspNetCoreApi/Controllers/StatesController" };
        }

    }
}
