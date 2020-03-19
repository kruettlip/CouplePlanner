using System;
using System.Collections.Generic;
using CouplePlanner.Application.Entities;
using CouplePlanner.Application.Interfaces.Services;
using CouplePlanner.Application.Schema;
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

    public IEventApplicationService EventApplicationService { get; set; }

    public EventsController(ISchemaProvider schemaProvider, IEventApplicationService eventApplicationService)
    {
      SchemaProvider = schemaProvider;
      EventApplicationService = eventApplicationService;
    }

    /// <summary>
    /// Returns all events
    /// </summary>
    /// <returns>List of Events</returns>
    [HttpGet]
    public ActionResult<IEnumerable<Event>> GetAll()
    {
      try
      {
        return Ok(EventApplicationService.GetAll());
      }
      catch (Exception)
      {
        return BadRequest();
      }
    }

    /// <summary>
    /// Get the JSON-Schema of the event-entity
    /// </summary>
    /// <returns>JSON-Schema</returns>
    [HttpGet("schema")]
    public ActionResult<string> GetSchema()
    {
      try
      {
        var schema = SchemaProvider.GetSchema<Event>();

        return Ok(schema);
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }
  }
}
