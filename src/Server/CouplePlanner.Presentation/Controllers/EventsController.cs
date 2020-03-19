using System;
using CouplePlanner.Presentation.Entities;
using CouplePlanner.Presentation.Schema;
using Microsoft.AspNetCore.Mvc;

namespace CouplePlanner.Presentation.Controllers
{
  /// <summary>
  /// Manage events that the couple plans together
  /// </summary>
  [Route("api/[controller]")]
  [ApiController]
  public class EventsController : ControllerBase
  {
    public ISchemaProvider SchemaProvider { get; set; }

    public EventsController(ISchemaProvider schemaProvider)
    {
      SchemaProvider = schemaProvider;
    }

    /// <summary>
    /// Is just here for testing a get-method
    /// </summary>
    /// <returns>"Hello World"</returns>
    [HttpGet]
    public ActionResult<string> Test()
    {
      return Ok("Hello World");
    }

    [HttpGet("schema")]
    public ActionResult<string> GetSchema()
    {
      try
      {
        var schema = SchemaProvider.GetSchema<MyObject>();

        return Ok(schema.ToJson());
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }

    /// <summary>
    /// Is just here for testing a post-method
    /// </summary>
    /// <param name="myObject">The object that will be validated</param>
    /// <returns>The passed object, if validation passes</returns>
    [HttpPost]
    public IActionResult TestPost(MyObject myObject)
    {
      return Ok(myObject);
    }
  }
}
