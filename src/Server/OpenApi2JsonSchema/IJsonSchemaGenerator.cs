using NJsonSchema;
using NJsonSchema.Generation;

namespace kruettlip.OpenApi2JsonSchema
{
  public interface IJsonSchemaGenerator
  {
    JsonSchema GetSchemaWithOpenApi<T>(string openApiUrl);

    JsonSchema GetSchema<T>(JsonSchemaGeneratorSettings generatorSettings = null);
  }
}
