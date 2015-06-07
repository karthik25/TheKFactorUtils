using System.Web.Routing;

namespace TheKFactorUtils.Mvc.Routing
{
    public class RouteDataWrapper
    {
        private readonly RouteData _routeData;

        public RouteDataWrapper(RouteData routeData)
        {
            _routeData = routeData;
        }

        public string Controller
        {
            get { return _routeData.Values["controller"].ToString(); }
        }

        public string Action
        {
            get { return _routeData.Values["action"].ToString(); }
        }
    }
}