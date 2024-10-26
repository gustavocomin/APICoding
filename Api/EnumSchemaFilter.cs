using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel;
using System.Reflection;

namespace API_Coding
{
    /// <summary>
    /// 
    /// </summary>
    public class EnumSchemaFilter : ISchemaFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                var enumType = context.Type;
                schema.Enum.Clear();

                foreach (var field in enumType.GetFields(BindingFlags.Public | BindingFlags.Static))
                {
                    var descriptionAttribute = field.GetCustomAttribute<DescriptionAttribute>();
                    var description = descriptionAttribute?.Description ?? field.Name;

                    schema.Enum.Add(new OpenApiString($"{field.GetRawConstantValue()} - {description}"));
                }
            }
        }
    }
}