using System.Text.RegularExpressions;

namespace UniLetters.WebApi.Endpoints.Constraints;

public partial class AmConstraint : IRouteConstraint
{
    private static readonly Regex Regex = AmRegex();

    public bool Match(
        HttpContext? httpContext, 
        IRouter? route, 
        string routeKey,
        RouteValueDictionary values, 
        RouteDirection routeDirection)
    {
        if (values.TryGetValue(routeKey, out var value) && value is string stringValue)
        {
            return Regex.IsMatch(stringValue);
        }
        
        return false;
    }

    [GeneratedRegex("^[a-zA-Z][0-9]{5}$")]
    private static partial Regex AmRegex();
}
