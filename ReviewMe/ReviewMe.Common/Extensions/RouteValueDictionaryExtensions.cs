using System.Web.Routing;

namespace ReviewMe.Common.Extensions
{
    public static class RouteValueDictionaryExtensions
    {
        public static void MergeWith(this RouteValueDictionary dictionary, object values)
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