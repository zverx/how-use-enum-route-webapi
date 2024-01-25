using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;

namespace WebApp.Constraints
{
    public class EnumConstraint : IRouteConstraint
    {
        private readonly Type _enumType;
        public EnumConstraint(string enumTypeName)
        {
            var enumLocation = typeof(Enums.Index);

            if (enumLocation.Namespace is null)
                throw new Exception("enumLocation.Namespace is null");

            var enums = enumLocation
                .Assembly
                .GetTypes()
                .Where(t => t.IsEnum && t.Namespace == enumLocation.Namespace);

            _enumType = enums.Single(e => e.Name == enumTypeName);
        }
        public bool Match(
            HttpContext? httpContext,
            IRouter? route,
            string routeKey,
            RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            if (values.TryGetValue(routeKey, out object? value))
            {
                if (value != null && Enum.TryParse(_enumType, $"{value}", true, out object? result))
                {
                    return result != null && Enum.IsDefined(_enumType, result);
                }
            }

            return false;
        }
    }
}
