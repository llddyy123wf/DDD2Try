using System.Web.Mvc;
using Framework.Library.Mvc.Captcha;
using Framework.Library.Web.Captcha;

namespace PhotoMaps.UserCenter.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult CheckCode(string code)
        {
            ICaptchaImage image = new CaptchaImage();
            CaptchaResult result = new CaptchaResult(image, 90, 200, 60);

            result.ExecuteResult(this.ControllerContext.HttpContext.Response);
            return View();
        }
    }
}
