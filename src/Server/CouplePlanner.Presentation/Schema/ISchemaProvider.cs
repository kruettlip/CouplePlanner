using NJsonSchema;

namespace CouplePlanner.Presentation.Schema
{
  public interface ISchemaProvider
  {
    JsonSchema GetSchema<T>();
  }
}
