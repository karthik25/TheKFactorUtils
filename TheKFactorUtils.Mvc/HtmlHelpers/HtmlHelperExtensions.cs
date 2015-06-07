using System.Web.Mvc;

namespace TheKFactorUtils.Mvc.HtmlHelpers
{
    public static class HtmlHelperExtensions
    {
        public static string GetRequiredString(this HtmlHelper htmlHelper, string requiredString)
        {
            return htmlHelper.ViewContext
                             .RouteData
                             .GetRequiredString(requiredString);
        }

        public static bool IsAuthenticated(this HtmlHelper htmlHelper)
        {
            return htmlHelper.ViewContext
                             .RequestContext
                             .HttpContext
                             .Request
                             .IsAuthenticated;
        }

        public static UrlHelper GetUrlHelper(this HtmlHelper htmlHelper)
        {
            return new UrlHelper(htmlHelper.ViewContext
                                           .RequestContext);
        }
    }
}