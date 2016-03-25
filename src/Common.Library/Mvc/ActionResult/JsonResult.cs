using System;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Framework.Library.Mvc
{
    public class CustomJsonResult : JsonResult
    {
        private ISerializer serializer;
        public CustomJsonResult(object data, ISerializer serializer)
        {
            this.Data = data;
            this.serializer = serializer;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = String.IsNullOrEmpty(ContentType) ? "application/json" : ContentType;
            response.ContentEncoding = ContentEncoding != null ? ContentEncoding : Encoding.Default;

            if (Data != null)
            {
                string result = this.serializer.SerializeJson(Data);
                response.Write(result);
            }
        }
    }
}
