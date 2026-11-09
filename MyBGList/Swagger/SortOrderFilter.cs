using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using MyBGList.Attributes;

namespace MyBGList.Swagger
{
    public class SortOrderFilter : IParameterFilter
    {
        public void Apply(
            OpenApiParameter parameter,
            ParameterFilterContext context)
        {
            // Attributes can be on an action parameter (ParameterInfo)
            // or on a property of a [FromQuery] DTO (PropertyInfo).
            var attributes = (context.ParameterInfo?.GetCustomAttributes(true) ?? Array.Empty<object>())
                .Union(context.PropertyInfo?.GetCustomAttributes(true) ?? Array.Empty<object>())
                .OfType<SortOrderValidatorAttribute>();

            foreach (var attribute in attributes)
            {
                parameter.Schema.Extensions["pattern"] =
                    new OpenApiString(string.Join("|",
                        attribute.AllowedValues.Select(v => $"^{v}$")));
            }
        }
    }
}