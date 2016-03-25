using System.Web.Mvc;

namespace Framework.Library.Mvc
{
    public class XmlResult : ActionResult
    {
        private ISerializer serializer;
        private object data;
        public XmlResult(object data)
        {
            this.data = data;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.ContentType = "text/xml";
            if (this.data != null)
            {
                string result = serializer.SerializeXml(this.data);
                context.HttpContext.Response.Write(result);
            }
        }
    }
}
