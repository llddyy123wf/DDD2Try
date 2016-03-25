using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoMaps.Web
{
    public class BaseController : Controller
    {
        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding)
        {
            return new BaseJsonResult
            {
                Data = data,
                ContentEncoding = contentEncoding,
                ContentType = contentType,
            };
        }
    }
}