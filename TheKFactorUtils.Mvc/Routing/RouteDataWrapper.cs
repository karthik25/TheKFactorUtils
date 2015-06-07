using System.Linq;
using System.Web.Routing;

namespace TheKFactorUtils.Mvc.Routing
{
    public class RouteDataWrapper
    {
        protected readonly RouteData _routeData;

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

        public RouteValueDictionary Values
        {
            get { return _routeData.Values; }
        }

        public string this[string key]
        {
            get { return _routeData.Values.ContainsKey(key) ? _routeData.Values[key].ToString() : null; }
        }

        protected string GetSafeValue(string propName)
        {
            return (from name in propName.GetModifiedPropertyNames()
                    where _routeData.Values.ContainsKey(name)
                    select _routeData.Values[name].ToString()).FirstOrDefault();
        }
    }    
}
