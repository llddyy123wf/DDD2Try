using System;
using System.Web.Mvc;

namespace Framework.Library.Mvc
{
    /// <summary>
    /// 自定义错误类型
    /// </summary>
    /// <remarks>
    /// 可以记录日志获取错误的Controller及Action
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class CustomExceptionFilterAttribute : FilterAttribute, IExceptionFilter
    {
        public event Action<HandleErrorInfo> FoundException;
        private const string DefaultView = "Error";
        private string view = string.Empty;
        
        /// <summary>
        /// 转向View视图, 默认为Error
        /// </summary>
        public string View 
        { 
            get
            {
                return string.IsNullOrEmpty(this.view) ? DefaultView : this.view;
            } 
            set
            {
                this.view = value; 
            }
        }

        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null) throw new ArgumentNullException("filterContext");
            if (filterContext.IsChildAction) return;
            if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled) return;

            UrlHelper url = new UrlHelper(filterContext.RequestContext);
            HandleErrorInfo model = new HandleErrorInfo(filterContext.Exception, filterContext.RouteData.Values["controller"].ToString(), filterContext.RouteData.Values["action"].ToString());

            if (this.FoundException != null) this.FoundException(model);

            filterContext.Result = new ViewResult
            {
                ViewName = this.View,
                ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
                TempData = filterContext.Controller.TempData,
            };

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }
    }
}
