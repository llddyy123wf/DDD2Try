using PhotoMaps.ViewModel;
using PhotoMaps.Web;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PhotoMaps.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Title = "我的地图";
            return View();
        }
    }
}