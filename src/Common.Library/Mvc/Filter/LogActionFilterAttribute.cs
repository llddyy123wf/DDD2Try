using System.Diagnostics;
using System.Web.Mvc;

namespace Framework.Library.Mvc
{
    public class LogActionFilterAttribute : ActionFilterAttribute
    {
        private const string stopwatchKey = "StopwatchFilter.Key";
        public ILog Logger { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.HttpContext.Application[stopwatchKey] = Stopwatch.StartNew();
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var stopwatch = (Stopwatch)filterContext.HttpContext.Application[stopwatchKey];
            stopwatch.Stop();

            var log = string.Format("controller:{0}, action:{1}， execute time: {2}ms",
                filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                filterContext.ActionDescriptor.ActionName,
                stopwatch.ElapsedMilliseconds);

            this.Logger.Info(log);
        }
    }
}
