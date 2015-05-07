using System.Net;
using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TEK.Recruit.WebApp.Controllers
{
    public class HealthController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        public HttpStatusCodeResult Index()
        {
            return new HttpStatusCodeResult((int)HttpStatusCode.OK);
        }
    }
}
