using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using ExpressionHelper = Microsoft.Web.Mvc.Internal.ExpressionHelper;

namespace ReviewMe.Common.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string ActionLink<TController>(this UrlHelper helper, Expression<Action<TController>> action,
            object extraRouteValues = null) where TController : Controller
        {
            RouteValueDictionary routeValuesFromExpression = ExpressionHelper.GetRouteValuesFromExpression(action);

            routeValuesFromExpression.Mergewith(extraRouteValues);

            return helper.Action(null, routeValuesFromExpression);
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