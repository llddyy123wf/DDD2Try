using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using DotNetOpenAuth.OpenId.RelyingParty;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PhotoMaps.Web.Controllers
{
    public class AccountController : Controller
    {
        private const string RolesAttribute = "http://samples.dotnetopenauth.net/sso/roles";
        private static OpenIdRelyingParty relyingParty = new OpenIdRelyingParty();
        public ActionResult LogOn()
        {
            if (Request.AcceptTypes.Contains("application/xrds+xml"))
            {
                return View("Xrds");
            }

            UriBuilder returnToBuilder = new UriBuilder(Request.Url);
            returnToBuilder.Path = "/Account/LogOn";
            returnToBuilder.Query = null;
            returnToBuilder.Fragment = null;
            Uri returnTo = returnToBuilder.Uri;
            returnToBuilder.Path = "/Account/LogOn";
            Realm realm = returnToBuilder.Uri;

            var response = relyingParty.GetResponse();
            // 获取Endpoint(OP)的相应, 如果为空, 则为首次登陆
            if (response == null)
            {
                if (Request.QueryString["ReturnUrl"] != null && User.Identity.IsAuthenticated)
                {
                    // The user must have been directed here because he has insufficient
                    // permissions to access something.
                    this.ViewBag.Message = "1";
                }
                else
                {
                    // Stage 2: user submitting Identifier
                    // 向OP的地址发起一个Http请求
                    // Because this is a sample of a controlled SSO environment,
                    // we don't ask the user which Provider to use... we just send
                    // them straight off to the one Provider we trust.
                    var request = relyingParty.CreateRequest(
                        ConfigurationManager.AppSettings["SsoProviderOPIdentifier"],
                        realm,
                        returnTo);
                    var fetchRequest = new FetchRequest();
                    fetchRequest.Attributes.AddOptional(RolesAttribute);
                    request.AddExtension(fetchRequest);
                    request.RedirectToProvider();
                }
            }
            else
            {
                // Stage 3: OpenID Provider sending assertion response
                // OP已经被操作过, 具体情况看状态码
                switch (response.Status)
                {
                    case AuthenticationStatus.Canceled:
                        this.ViewBag.Message = "Login canceled.";
                        break;
                    case AuthenticationStatus.Failed:
                        this.ViewBag.Message = HttpUtility.HtmlEncode(response.Exception.Message);
                        break;
                    // 成功
                    case AuthenticationStatus.Authenticated:
                        IList<string> roles = null;
                        var fetchResponse = response.GetExtension<FetchResponse>();
                        if (fetchResponse != null)
                        {
                            if (fetchResponse.Attributes.Contains(RolesAttribute))
                            {
                                roles = fetchResponse.Attributes[RolesAttribute].Values;
                            }
                        }
                        if (roles == null)
                        {
                            roles = new List<string>(0);
                        }

                        // Apply the roles to this auth ticket
                        const int TimeoutInMinutes = 100; // TODO: look up the right value from the web.config file
                        var ticket = new FormsAuthenticationTicket(
                            2,
                            response.ClaimedIdentifier,
                            DateTime.Now,
                            DateTime.Now.AddMinutes(TimeoutInMinutes),
                            false, // non-persistent, since login is automatic and we wanted updated roles
                            string.Join(";", roles.ToArray()));
                        // 设置Cookie
                        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
                        Response.SetCookie(cookie);
                        // 跳转到上次使用的页面
                        Response.Redirect(Request.QueryString["ReturnUrl"] ?? FormsAuthentication.DefaultUrl);
                        break;
                    default:
                        break;
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Xrds()
        {
            return View();
        }
    }
}
