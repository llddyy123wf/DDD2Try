using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId.Provider;
using PhotoMaps.UserCenter.Code;
using System.Linq;
using System.Web.Mvc;

namespace PhotoMaps.UserCenter.Controllers
{
    public class OpenIdController : Controller
    {
        internal static OpenIdProvider openIdProvider = new OpenIdProvider();

        public ActionResult Identifier()
        {
            if (User.Identity.IsAuthenticated && ProviderEndpoint.PendingAuthenticationRequest != null)
            {
                AuthenticationUtil.ProcessAuthenticationChallenge(ProviderEndpoint.PendingAuthenticationRequest);
                if (ProviderEndpoint.PendingAuthenticationRequest.IsAuthenticated.HasValue)
                {
                    ProviderEndpoint.SendResponse();
                }
            }

            if (Request.AcceptTypes.Contains("application/xrds+xml"))
            {
                Server.TransferRequest("~/OpenId/Xrds");
                //return new TransferResult("~/OpenId/Xrds");
            }

            return View();
        }

        [ValidateInput(false)]
        public ActionResult Provider()
        {
            var request = openIdProvider.GetRequest();

            if (request != null)
            {
                if (request.IsResponseReady)
                {
                    return openIdProvider.PrepareResponse(request).AsActionResult();
                }

                ProviderEndpoint.PendingRequest = (IHostProcessedRequest)request;
                var idrequest = request as IAuthenticationRequest;

                return AuthenticationUtil.ProcessAuthenticationChallenge(idrequest);
            }

            return View();
        }

        public ActionResult AskUser()
        {
            return View();
        }

        public ActionResult Xrds()
        {
            return View();
        }
    }
}
