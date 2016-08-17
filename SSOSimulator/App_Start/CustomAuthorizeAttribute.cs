using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace SSOSimulator.App_Start
{
    [AttributeUsage(AttributeTargets.All)]
    public sealed class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        #region Override

        /// <summary>
        /// On authorization.
        /// </summary>
        /// <param name="filterContext"> Parameter value.</param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            bool skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
            || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);
            if (!skipAuthorization)
            {
                base.OnAuthorization(filterContext);
                
                if (filterContext.HttpContext.Session["UserLogin"] == null)
                {
                    var routeValue = new
                    {
                        controller = "Account",
                        action = "LogIn",
                    };

                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(routeValue));
                }
            }
        }

        #endregion Override
    }
}