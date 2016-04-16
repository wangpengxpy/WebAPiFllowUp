using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Http;

namespace WebAPiFllowUp.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class BasicAuthenticationFilter : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var userIdentity = ParseHeader(actionContext);
            if (userIdentity == null)
            {
                Challenge(actionContext);
                return;
            }

            var principal = new GenericPrincipal(userIdentity, null);

            Thread.CurrentPrincipal = principal;

            if (!OnAuthorizeUser(userIdentity.Name, userIdentity.UserPassword, actionContext))
            {
                Challenge(actionContext);
                return;
            }


          

            base.OnAuthorization(actionContext);
        }

        protected virtual bool OnAuthorizeUser(string userName, string userPassword, HttpActionContext actionContext)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(userPassword))

                return false;
            else
                return true;

        }

        public virtual BasicAuthenticationIdentity ParseHeader(HttpActionContext actionContext)
        {
            string authParameter = null;

            var authValue = actionContext.Request.Headers.Authorization;
            if (authValue != null && authValue.Scheme == "Basic")
                authParameter = authValue.Parameter;

            if (string.IsNullOrEmpty(authParameter))

                return null;

            authParameter = Encoding.Default.GetString(Convert.FromBase64String(authParameter));

            var authToken = authParameter.Split(':');
            if (authToken.Length < 2)
                return null;

            return new BasicAuthenticationIdentity(authToken[0], authToken[1]);
        }

        private void Challenge(HttpActionContext actionContext)
        {
            var host = actionContext.Request.RequestUri.DnsSafeHost;
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            actionContext.Response.Headers.Add("WWW-Authenticate", string.Format("Basic realm=\"{0}\"", host));

        }
    }
}