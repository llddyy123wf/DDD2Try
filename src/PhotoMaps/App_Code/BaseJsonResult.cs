using Newtonsoft.Json;
using System;
using System.Web;
using System.Web.Mvc;

namespace PhotoMaps.Web
{
    public class BaseJsonResult : JsonResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            HttpResponseBase response = context.HttpContext.Response;
            if (!String.IsNullOrEmpty(ContentType))
            {
                response.ContentType = ContentType;
            }
            else
            {
                response.ContentType = "application/json";
            }

            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }

            if (Data != null)
            {
                string result = JsonConvert.SerializeObject(Data);
                response.Write(result);
            }
        }
    }
}