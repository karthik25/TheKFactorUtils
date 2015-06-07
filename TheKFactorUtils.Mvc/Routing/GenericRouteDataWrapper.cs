using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Routing;

namespace TheKFactorUtils.Mvc.Routing
{
    public class RouteDataWrapper<T> : RouteDataWrapper
        where T : class, new()
    {
        public RouteDataWrapper(RouteData routeData)
            : base(routeData)
        {

        }

        public T Instance
        {
            get { return CreateInstance(); }
        }

        private T CreateInstance()
        {
            var instance = new T();
            var properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var propertyInfo in properties)
            {
                var propName = propertyInfo.Name;
                var value = GetSafeValue(propName);
                propertyInfo.SetValue(instance, value);
            }
            return instance;
        }

        public string Get<TProperty>(Expression<Func<T, TProperty>> propertySelector)
        {
            if (propertySelector == null)
                throw new Exception("Property selector cannot be null");

            var memberExp = propertySelector.Body as MemberExpression;
            if (memberExp == null)
                throw new ArgumentException("The expression's body must be a MemberExpression. The code block supplied should identify a property.\nExample: x => x.Bar.", "propertySelector");

            var prop = memberExp.Member as PropertyInfo;
            if (prop == null)
                throw new ArgumentException("The expression's body must identify a property, not a field or other member.", "propertySelector");

            var propName = prop.Name;
            return GetSafeValue(propName);
        }

        private string GetSafeValue(string propName)
        {
            return (from name in propName.GetModifiedPropertyNames() 
                    where _routeData.Values.ContainsKey(name) 
                    select _routeData.Values[name].ToString()).FirstOrDefault();
        }
    }
}