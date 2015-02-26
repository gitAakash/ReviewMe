using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using ExpressionHelper = Microsoft.Web.Mvc.Internal.ExpressionHelper;

namespace ReviewMe.Common.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString ActionLinkFor<TController>(this HtmlHelper helper, string linkText,
            Expression<Action<TController>> action,
            object extraRouteValues = null,
            object htmlAttributes = null)
            where TController : Controller
        {
            RouteValueDictionary routeValuesFromExpression = ExpressionHelper.GetRouteValuesFromExpression(action);

            routeValuesFromExpression.Mergewith(extraRouteValues);

            return new MvcHtmlString(
                HtmlHelper.GenerateLink(
                    helper.ViewContext.RequestContext,
                    helper.RouteCollection,
                    linkText,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    routeValuesFromExpression,
                    new RouteValueDictionary(htmlAttributes)));
        }

        public static void RenderActionFor<TController>(this HtmlHelper helper, Expression<Action<TController>> action,
            object extraRouteValues = null)
            where TController : Controller
        {
            RouteValueDictionary routeValuesFromExpression = ExpressionHelper.GetRouteValuesFromExpression(action);

            string actionName = routeValuesFromExpression["Action"].ToString();
            string controllerName = routeValuesFromExpression["Controller"].ToString();

            helper.RenderAction(actionName, controllerName, routeValuesFromExpression);
        }

        private static void Mergewith(this RouteValueDictionary dictionary, object values)
        {
            if (values == null)
                return;

            var valueDictionary = new RouteValueDictionary(values);

            foreach (var pair in valueDictionary)
            {
                dictionary[pair.Key] = pair.Value;
            }
        }
    }
}