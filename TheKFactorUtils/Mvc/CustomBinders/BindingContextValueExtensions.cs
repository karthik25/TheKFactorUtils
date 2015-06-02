using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace TheKFactorUtils.Mvc.CustomBinders
{
    public static class BindingContextValueExtensions
    {
        public static IEnumerable<string> GetPostedFormKeys(this ControllerContext controllerContext)
        {
            var formKeys = controllerContext.HttpContext
                                            .Request
                                            .Form
                                            .AllKeys.ToList();
            return formKeys;
        }

        public static string GetValue(this ModelBindingContext bindingContext, string keyName)
        {
            if (bindingContext == null || bindingContext.ValueProvider == null)
                throw new NullReferenceException("Binding context is null");

            var result = bindingContext.ValueProvider.GetValue(keyName);
            if (result != null)
            {
                var values = (string[]) result.RawValue;
                return values.First();
            }
            return null;
        }
    }
}