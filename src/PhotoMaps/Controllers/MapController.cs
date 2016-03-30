using PhotoMaps.Application;
using PhotoMaps.Domain;
using Framework.Infrastructure;
using PhotoMaps.ViewModel;
using System.Collections.Generic;
using System.Web.Mvc;
using Framework.Infrastructure.Message;
using Framework.Infrastructure.Result;

namespace PhotoMaps.Web.Controllers
{
    public class MapController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Title = "我的地图";

            return View();
        }
        
        public ActionResult MapPhoto(decimal swLng, decimal swLat, decimal neLng, decimal neLat, int level)
        {
            PhotoApplication app = PhotoApplication.Instance;
            PhotoCoordinate swCoor = new PhotoCoordinate() { Latitude = swLat, Longitude = swLng };
            PhotoCoordinate neCoor = new PhotoCoordinate() { Latitude = neLat, Longitude = neLng };
            IEnumerable<Photo> result = app.LoadByCoordinates(swCoor, neCoor, 1);

            List<PhotoView> view = new List<PhotoView>();
            foreach (var r in result)
            {
                PhotoView v = new PhotoView();
                v.Parse(r);
                view.Add(v);
            }

            return new BaseJsonResult
            {
                Data = view,
            };
        }

        public ActionResult Save(PhotoInfo info)
        {
            Photo photo = info.Parse();
            PhotoApplication app = PhotoApplication.Instance;
            app.Save(photo);

            MessageResult<string> result = new MessageResult<string>()
            {
                Success = true,
                Message = "保存成功",
            };

            return new BaseJsonResult 
            {
                 Data = result,
            };
        }
    }
}
