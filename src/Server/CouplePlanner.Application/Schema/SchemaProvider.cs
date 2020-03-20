using System;
using kruettlip.OpenApi2JsonSchema;
using NJsonSchema;

namespace CouplePlanner.Application.Schema
{
  public class SchemaProvider : ISchemaProvider
  {
    public IJsonSchemaGenerator JsonSchemaGenerator { get; set; }

    public SchemaProvider(IJsonSchemaGenerator schemaGenerator)
    {
      JsonSchemaGenerator = schemaGenerator;
    }

    public JsonSchema GetSchema<T>()
    {
      var schema = JsonSchemaGenerator.GetSchemaWithOpenApi<T>("http://localhost:20220/swagger/v1/swagger.json");

      ConsiderSpecificType(
        typeof(int?),
        typeof(T),
        schema,
        (propertyName, currentSchema) =>
        {
          var correctedPropertyName = CorrectPropertyName(propertyName);
          currentSchema.Properties[correctedPropertyName].Type = JsonObjectType.Number;
          currentSchema.Properties[correctedPropertyName].Format = null;
        });
      return schema;
    }

    private static void ConsiderSpecificType(Type propertyType, Type parentObject, JsonSchema schema,
      Action<string, JsonSchema> action)
    {
      if (action == null)
      {
        return;
      }

      var propertyInfos = parentObject.GetProperties();

      foreach (var currentPropertyInfo in propertyInfos)
      {
        if (currentPropertyInfo.PropertyType != propertyType)
        {
          continue;
        }

        action(currentPropertyInfo.Name, schema);
      }
    }

    private static string CorrectPropertyName(string propertyNameToCorrect)
    {
      return char.ToLowerInvariant(propertyNameToCorrect[0]) + propertyNameToCorrect.Substring(1);
    }
  }
}
