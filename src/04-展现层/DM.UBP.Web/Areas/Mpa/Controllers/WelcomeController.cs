using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using DM.UBP.Web.Controllers;

namespace DM.UBP.Web.Areas.Mpa.Controllers
{
    [AbpMvcAuthorize]
    public class WelcomeController : UbpControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}