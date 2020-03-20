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
    /// Get all events
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
    /// Add a new event
    /// </summary>
    /// <param name="newEvent">Event to create</param>
    /// <returns>ID of the created event</returns>
    [HttpPost]
    public ActionResult<Guid> Add([FromBody] Event newEvent)
    {
      try
      {
        return Ok(EventApplicationService.Create(newEvent));
      }
      catch (Exception)
      {
        return BadRequest();
      }
    }

    /// <summary>
    /// Delete an existing event by ID
    /// </summary>
    /// <param name="id">ID of the event to delete</param>
    /// <returns>StatusCode 204 No Content</returns>
    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
      try
      {
        EventApplicationService.Delete(id);
        return NoContent();
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
