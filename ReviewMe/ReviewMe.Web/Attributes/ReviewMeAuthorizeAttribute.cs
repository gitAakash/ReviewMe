using System;
using System.Web.Mvc;

namespace ReviewMe.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    internal class ReviewMeAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var reviewMeAuthentication = new ReviewMeAuthentication(filterContext);
            reviewMeAuthentication.Authorize();
        }
    }
}