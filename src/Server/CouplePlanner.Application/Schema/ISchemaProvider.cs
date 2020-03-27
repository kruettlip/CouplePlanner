using NJsonSchema;

namespace CouplePlanner.Application.Schema
{
	public interface ISchemaProvider
	{
		JsonSchema GetSchema<T>();
	}
}
