using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using MyBGList.Attributes;

namespace MyBGList.Swagger
{
    public class SortColumnFilter : IParameterFilter
    {
        public void Apply(
            OpenApiParameter parameter,
            ParameterFilterContext context)
        {
            // Attributes can be on an action parameter (ParameterInfo)
            // or on a property of a [FromQuery] DTO (PropertyInfo).
            var attributes = (context.ParameterInfo?.GetCustomAttributes(true) ?? Array.Empty<object>())
                .Union(context.PropertyInfo?.GetCustomAttributes(true) ?? Array.Empty<object>())
                .OfType<SortColumnValidatorAttribute>();

            foreach (var attribute in attributes)
            {
                var pattern = attribute.EntityType
                    .GetProperties()
                    .Select(p => p.Name);
                parameter.Schema.Extensions["pattern"] =
                    new OpenApiString(string.Join("|",
                        pattern.Select(v => $"^{v}$")));
            }
        }
    }
}